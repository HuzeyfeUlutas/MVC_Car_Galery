using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Car_Galery.Context;
using Car_Galery.Entities;
using Car_Galery.Helpers;
using Car_Galery.Managers;
using Car_Galery.Managers.Abstract;
using Car_Galery.Models;
using Car_Galery.Models.ViewModels;
using PagedList;
using Type = System.Type;

namespace Car_Galery.Controllers
{
    public class RentCarController : Controller
    {
        private  DbContext db = new VehiclesContext();

        private IUnitOfWork unitOfWork;
        // GET: RentCar
        public ActionResult Index(int? PageNumber)
        {
            unitOfWork = new EFUnitOfWork(db);

            InventoryViewModel ıvm = new InventoryViewModel();

            ıvm.FilterModel = new FilterModel();

            #region Vehicle List Model Binding
            List<VehicleModel> vhList = new List<VehicleModel>();

            vhList = unitOfWork.GetRepository<Vehicle>().GetAll(v=>v.Rentable == true && v.Rented == false).ProjectTo<VehicleModel>().ToList();

            ıvm.FilterModel.ResultCount = vhList.Count();

            ıvm.PagedVehicleModels = vhList.ToPagedList(PageNumber ?? 1, 6);
            #endregion

            #region Type Model  Brand Binding
            ıvm.TypeModels = unitOfWork.GetRepository<Entities.Type>().GetAll().ProjectTo<TypeModel>().ToList();
            ıvm.BrandModels = new List<BrandModel>();
            ıvm.ModelModels = new List<ModelModel>();
            #endregion

            #region Filter Model Binding
            
            ıvm.FilterModel.Filtered = false;
            
            #endregion
            
            #region Brand Categories Model Binding
            ıvm.BrandModelModels = unitOfWork.GetRepository<Brand>().GetAll().ProjectTo<BrandModelsModel>().ToList();

            foreach (var BrandModelModel in ıvm.BrandModelModels)
            {
                BrandModelModel.brandModels= unitOfWork.GetRepository<Model>()
                    .GetAll(ty => ty.BrandId == BrandModelModel.BrandId).ProjectTo<ModelModel>().ToList();
            }
            #endregion

            unitOfWork.Dispose();
            return View(ıvm);
        }

        public PartialViewResult FilterVehicle(FilterModel fm)
        {
            unitOfWork = new EFUnitOfWork(db);

            InventoryViewModel ıvm = new InventoryViewModel();

            var query = VehicleListHelper.Filter(fm, unitOfWork,true);

            List<VehicleModel> vhList = new List<VehicleModel>();

            vhList = query.ProjectTo<VehicleModel>().ToList();

            fm.ResultCount = vhList.Count();
            
            ıvm.PagedVehicleModels = vhList.ToPagedList(fm.PageNumber ?? 1, 6);

            int k = ıvm.PagedVehicleModels.Count;

            ıvm.FilterModel = fm;
            
            unitOfWork.Dispose();
            return PartialView("_VehicleListRACPartial", ıvm);
        }
    }
}