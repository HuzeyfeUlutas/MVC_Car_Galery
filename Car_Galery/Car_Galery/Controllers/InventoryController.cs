using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
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

            #region Vehicle List Model Binding

            
            ıvm.PagedVehicleModels = unitOfWork.GetRepository<Vehicle>().GetAll().ProjectTo<VehicleModel>().ToList().ToPagedList(PageNumber ?? 1, 6);

            #endregion

            ıvm.TypeModels = unitOfWork.GetRepository<Type>().GetAll().ProjectTo<TypeModel>().ToList();
            ıvm.FilterModel = new FilterModel();
            ıvm.FilterModel.Filtered = false;

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

            if(fm.Filtered == true)
            {
                if (fm.TypeId != null)
                {
                    query = query.Where(v => v.TypeId == fm.TypeId);
                }

                if (fm.BrandId != null)
                {
                    query = query.Where(v => v.BrandId == fm.BrandId);
                }

                if (fm.ModelId != null)
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
            else if (fm.BrandId != null)
            {
                query = query.Where(v => v.BrandId == fm.BrandId);

                if (fm.ModelId != null)
                {
                    query = query.Where(v => v.ModelId == fm.ModelId);
                }
            }
            if(!String.IsNullOrWhiteSpace(fm.SearchText))
            {
                query = query.Where(v => v.Name.Contains(fm.SearchText));
            }

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
            



            ıvm.PagedVehicleModels = query.ProjectTo<VehicleModel>().ToList().ToPagedList(fm.PageNumber ?? 1, 6);

            ıvm.FilterModel = fm;
            
            unitOfWork.Dispose();
            return PartialView("_VehicleListPartial", ıvm);
        }

    }
}