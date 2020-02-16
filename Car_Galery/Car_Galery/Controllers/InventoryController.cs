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
using Type = Car_Galery.Entities.Type;


namespace Car_Galery.Controllers
{
    public class InventoryController : Controller
    {
        private  DbContext db = new VehiclesContext();

        private EFUnitOfWork unitOfWork;

        // GET: Inventory
        public ActionResult Index()
        { 
            unitOfWork = new EFUnitOfWork(db);

            InventoryViewModel ıvm = new InventoryViewModel();

            #region Vehicle List Model Binding

            ıvm.VehicleModels = unitOfWork.GetRepository<Vehicle>().GetAll().ProjectTo<VehicleModel>().ToList();
            

            #endregion

            ıvm.TypeModels = unitOfWork.GetRepository<Type>().GetAll().ProjectTo<TypeModel>().ToList();


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

        public PartialViewResult FilterVehicleByBrandCategories( int brandID , int modelID)
        {

            unitOfWork = new EFUnitOfWork(db);
            List<VehicleModel> vehicleList = new List<VehicleModel>();

            if (modelID != 0)
            {
                vehicleList = unitOfWork.GetRepository<Vehicle>().GetAll(v => v.ModelId == modelID).ProjectTo<VehicleModel>().ToList();

                return PartialView("_VehicleListPartial", vehicleList);
            }
            vehicleList = unitOfWork.GetRepository<Vehicle>().GetAll(v => v.BrandId == brandID).ProjectTo<VehicleModel>().ToList();
            
            unitOfWork.Dispose();
            return PartialView("_VehicleListPartial", vehicleList);
        }

        public PartialViewResult SearchFilterVehicle()
        {
            unitOfWork = new EFUnitOfWork(db);
            List<VehicleModel> vehicleList = new List<VehicleModel>();




            return PartialView("_VehicleListPartial", vehicleList);
        }
    }
}