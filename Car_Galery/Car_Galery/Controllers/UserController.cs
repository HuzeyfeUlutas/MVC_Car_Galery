using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Car_Galery.Entities;
using Car_Galery.Managers;
using Car_Galery.Managers.Abstract;
using Car_Galery.Models;
using Car_Galery.Models.ViewModels;
using Microsoft.AspNet.Identity;
using PagedList;

namespace Car_Galery.Controllers
{
    
    public class UserController : Controller
    {
        // GET: User
        private IUnitOfWork unitOfWork;
        private DbContext db = new ApplicationDbContext();
        private ApplicationDbContext UsersContext = new ApplicationDbContext();


        [Authorize(Roles = "Admin")]
        public ActionResult Index(int? PageNumber)
        {
            UserViewModel uvm = new UserViewModel();

            List<User> User = new List<User>();

            User = UsersContext.Users.Where(u=>!u.Roles.Select(r=>r.RoleId).Contains("1")).Select(u => new User()
            {
                Id = u.Id,
                Name = u.UserName,
                PhoneNumber = u.PhoneNumber
            }).ToList();

            uvm.UsersCount = User.Count();


            uvm.PagedListUsers = User.ToPagedList(PageNumber ?? 1, 10);
            return View(uvm);
        }

        [Authorize(Roles = "Admin")]
        public PartialViewResult GetRentRequestList(int? PageNumber)
        {
            unitOfWork = new EFUnitOfWork(db);
            UserRequestViewModel urvm = new UserRequestViewModel();

            List<UserRequestModel> userRequests = new List<UserRequestModel>();

            userRequests = unitOfWork.GetRepository<UserRequest>().GetAll().ProjectTo<UserRequestModel>().ToList();

            urvm.UsersRequestCount = userRequests.Count();

            urvm.PagedListUsers = userRequests.ToPagedList(PageNumber ?? 1, 10);

            unitOfWork.Dispose();
            return PartialView("_UserRequestListView",urvm) ;
        }

        [Authorize(Roles = "Admin")]
        public PartialViewResult GetUserList(int? PageNumber)
        {
            UserViewModel uvm = new UserViewModel();

            List<User> User = new List<User>();

            User = UsersContext.Users.Where(u=>!u.Roles.Select(r=>r.RoleId).Contains("1")).Select(u => new User()
            {
                Id = u.Id,
                Name = u.UserName,
                PhoneNumber = u.PhoneNumber
            }).ToList();

            uvm.UsersCount = User.Count();

            uvm.PagedListUsers = User.ToPagedList(PageNumber ?? 1, 10);


            return PartialView("_UserListPartial",uvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteUser(string id)
        {
            var entity = UsersContext.Users.Find(id);
            string name = entity.UserName;

            UsersContext.Users.Remove(entity);
            UsersContext.SaveChanges();

            return RedirectToAction("GetUserList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteUserRequest(int id, int vehicleId)
        {
            unitOfWork = new EFUnitOfWork(db);

            var entity = unitOfWork.GetRepository<Vehicle>().GetById(vehicleId);

            unitOfWork.GetRepository<UserRequest>().Delete(id);

            entity.Rented = false;

            unitOfWork.GetRepository<Vehicle>().Update(entity);

            unitOfWork.SaveChanges();
            unitOfWork.Dispose();
            return RedirectToAction("GetRentRequestList");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public PartialViewResult GetVehicleModal(int id, DateTime dt)
        {
            unitOfWork = new EFUnitOfWork(db);

            RequestVehicleModalViewModel rvmvm = new RequestVehicleModalViewModel();

            var entity = unitOfWork.GetRepository<Vehicle>().GetById(id);

            Mapper.Map(entity, rvmvm);

            rvmvm.RequestTime = dt;

            unitOfWork.Dispose();

            return PartialView("_RequestVehicleModalPartial", rvmvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult AddRequest(int VehicleId , string location)
        {
            var userId = User.Identity.GetUserId();

            var userentity = UsersContext.Users.Find(userId);

            unitOfWork = new EFUnitOfWork(db);

            UserRequest ur= new UserRequest();

            ur.Location = location;

            ur.RequestDateTime = DateTime.Now;

            ur.UserPhoneNumber = userentity.PhoneNumber;

            ur.UserName = userentity.UserName;

            ur.VehicleId = VehicleId;

            var vehicle = unitOfWork.GetRepository<Vehicle>().GetById(VehicleId);


            if (vehicle.Rented == true)
            {
                return Json(new {success = false, responseText = "Araç daha önce kiralanmış, Tekrar deneyin."},
                    JsonRequestBehavior.AllowGet);
            }else if (userentity.Balance < 200)
            {
                return Json(new {success = false, responseText = "Bakiyenizin 200 den fazla olması gerekiyor."},
                    JsonRequestBehavior.AllowGet);
            }else
            {
                userentity.Balance -= 200;
                vehicle.Rented = true;
                unitOfWork.GetRepository<Vehicle>().Update(vehicle);
                unitOfWork.GetRepository<UserRequest>().Add(ur);
                unitOfWork.SaveChanges();

                UsersContext.Entry(userentity).State = EntityState.Modified;
                UsersContext.SaveChangesAsync();
                
                unitOfWork.Dispose();
                return Json(new {success = true, responseText = "Request Success."},JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult AddBalance(int Balance)
        {
            var entity = UsersContext.Users.Find(User.Identity.GetUserId());

            entity.Balance += Balance;


            UsersContext.Entry(entity).State = EntityState.Modified;

            UsersContext.SaveChangesAsync();


            return RedirectToAction("Index", "Manage");
        }

        [Authorize(Roles = "User,Admin")]
        public ActionResult GetRequestPartial()
        {
            unitOfWork = new EFUnitOfWork(db);

            List<Vehicle>  v = new List<Vehicle>();

            var userName = User.Identity.GetUserName();

            var requestedVehicle = unitOfWork.GetRepository<UserRequest>().GetAll().Where(ur => ur.UserName == userName)
                .ToList();

            foreach (var request in requestedVehicle)
            {
                v.Add(unitOfWork.GetRepository<Vehicle>().GetById(request.VehicleId));
            }

            List<VehicleModel> vm = new List<VehicleModel>();

            vm = Mapper.Map<List<Vehicle>, List<VehicleModel>>(v);

            unitOfWork.Dispose();

            return PartialView("_UserRequestVehiclePartial",vm);
        }
    }
}