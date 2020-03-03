using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Car_Galery.Context;
using Car_Galery.Entities;
using Car_Galery.Managers;
using Car_Galery.Managers.Abstract;
using Car_Galery.Models;
using Car_Galery.Models.ViewModels;
using PagedList;

namespace Car_Galery.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        // GET: User
        private IUnitOfWork unitOfWork;
        private DbContext db = new VehiclesContext();
        private ApplicationDbContext UsersContext = new ApplicationDbContext();




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
    }
}