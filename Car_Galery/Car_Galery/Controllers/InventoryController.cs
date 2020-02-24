using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Car_Galery.Context;
using Car_Galery.Entities;
using Car_Galery.Managers;
using Car_Galery.Managers.Abstract;
using Car_Galery.Models;
using Car_Galery.Models.ViewModels;
using PagedList;
using Type = Car_Galery.Entities.Type;


namespace Car_Galery.Controllers
{
    public class InventoryController : Controller
    {
        private  DbContext db = new VehiclesContext();

        private EFUnitOfWork unitOfWork;

        // GET: Inventory
        public ActionResult Index(int? PageNumber)
        { 
            unitOfWork = new EFUnitOfWork(db);

            InventoryViewModel ıvm = new InventoryViewModel();

            ıvm.FilterModel = new FilterModel();

            #region Vehicle List Model Binding
            List<VehicleModel> vhList = new List<VehicleModel>();

            vhList = unitOfWork.GetRepository<Vehicle>().GetAll().ProjectTo<VehicleModel>().ToList();

            ıvm.FilterModel.ResultCount = vhList.Count();

            ıvm.PagedVehicleModels = vhList.ToPagedList(PageNumber ?? 1, 6);
            #endregion

            #region Type Model  Brand Binding
            ıvm.TypeModels = unitOfWork.GetRepository<Type>().GetAll().ProjectTo<TypeModel>().ToList();
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

            var query = unitOfWork.GetRepository<Vehicle>().GetAll().AsQueryable();


            #region Filter
            if(fm.Filtered == true)
            {
                int? ku = fm.TypeId;
                if (fm.TypeId != 0 && fm.TypeId != null)
                {
                    query = query.Where(v => v.TypeId == fm.TypeId);
                }

                ku = fm.BrandId;
                if (fm.BrandId != 0 && fm.BrandId != null)
                {
                    query = query.Where(v => v.BrandId == fm.BrandId);
                }

                ku = fm.ModelId;
                if (fm.ModelId != 0 && fm.ModelId != null)
                {
                    query = query.Where(v => v.ModelId == fm.ModelId);
                }

                if(!String.IsNullOrWhiteSpace(fm.FuelType))
                {
                    query = query.Where(v => v.Fuel == fm.FuelType);
                }

                if(!String.IsNullOrWhiteSpace(fm.Transmission))
                {
                    query = query.Where(v => v.Transmission == fm.Transmission);
                }

                if(!String.IsNullOrWhiteSpace(fm.Year))
                {
                    query = query.Where(v => v.Year == fm.Year);
                }

                if (fm.MinPrice != null)
                {
                    query = query.Where(v => v.Price >= fm.MinPrice);
                }

                if (fm.MaxPrice != null)
                {
                    query = query.Where(v => v.Price <= fm.MaxPrice);
                }

                if (fm.MinKm != null)
                {
                    query = query.Where(v => v.Km >= fm.MinKm);
                }

                if (fm.MaxKm != null)
                {
                    query = query.Where(v => v.Km <= fm.MaxKm);
                }

            }
            #endregion

            #region Brand Categories
            else if (fm.BrandId != 0 && fm.BrandId != null)
            {
                query = query.Where(v => v.BrandId == fm.BrandId);

                if (fm.ModelId != 0 && fm.ModelId != null)
                {
                    query = query.Where(v => v.ModelId == fm.ModelId);
                }
            }
            #endregion

            #region Search Text
            if(!String.IsNullOrWhiteSpace(fm.SearchText))
            {
                query = query.Where(v => v.Name.Contains(fm.SearchText));
            }
            #endregion

            #region Sort
            if (!String.IsNullOrWhiteSpace(fm.SortBy))
            {
                switch (fm.SortBy)
                {
                    case "Name":
                        query = query.OrderBy(v => v.Name);
                        break;
                    case  "Price":
                        query = query.OrderBy(v => v.Price);
                        break;
                    case "Km":
                        query = query.OrderBy(v => v.Km);
                        break;

                }
            }
            #endregion

            List<VehicleModel> vhList = new List<VehicleModel>();

            vhList = query.ProjectTo<VehicleModel>().ToList();

            fm.ResultCount = vhList.Count();
            
            ıvm.PagedVehicleModels = vhList.ToPagedList(fm.PageNumber ?? 1, 6);

            int k = ıvm.PagedVehicleModels.Count;

            ıvm.FilterModel = fm;
            
            unitOfWork.Dispose();
            return PartialView("_VehicleListPartial", ıvm);
        }

        public PartialViewResult GetVehicleModal(int? id)
        {
            unitOfWork = new EFUnitOfWork(db);
            var modal = unitOfWork.GetRepository<Vehicle>().GetById((int)id);

            VehicleModalViewModel vm = Mapper.Map<Vehicle, VehicleModalViewModel>(modal);

            string x = vm.BrandName;

            unitOfWork.Dispose();
            return PartialView("_VehicleModalPartial", vm);
        }

        public ActionResult FillBrands(int? TypeId)
        {
            unitOfWork = new EFUnitOfWork(db);

            List<BrandModel> brands = new List<BrandModel>();

            brands = unitOfWork.GetRepository<TypeBrand>().GetAll().Where(tb => tb.TypeId == TypeId)
                .Select(tb => tb.Brand).ProjectTo<BrandModel>().ToList();
            unitOfWork.Dispose();
            return Json(brands, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FillModels(int? BrandId)
        {
            unitOfWork = new EFUnitOfWork(db);

            List<ModelModel> models = new List<ModelModel>();

            models = unitOfWork.GetRepository<Model>().GetAll().Where(m => m.BrandId == BrandId).ProjectTo<ModelModel>()
                .ToList();
            unitOfWork.Dispose();
            return Json(models, JsonRequestBehavior.AllowGet);
        }

        
    }
}