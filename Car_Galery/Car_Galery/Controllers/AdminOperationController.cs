using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
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
    //[Authorize(Roles = "Admin")]
    public class AdminOperationController : Controller
    {

        private IUnitOfWork unitOfWork;
        private DbContext db = new VehiclesContext();
        // GET: AdminOperation

        #region GetModal
        public PartialViewResult GetTypeModal()
        {
            unitOfWork = new EFUnitOfWork(db);

            List<TypeModel> model = new List<TypeModel>();

            model = unitOfWork.GetRepository<Type>().GetAll().ProjectTo<TypeModel>().ToList();

            unitOfWork.Dispose();

            return PartialView("_TypePartialView",model);
        }

        public PartialViewResult GetBrandModal()
        {
            unitOfWork = new EFUnitOfWork(db);

            List<BrandModel> model = new List<BrandModel>();

            model = unitOfWork.GetRepository<Brand>().GetAll().ProjectTo<BrandModel>().ToList();

            return PartialView("_BrandPartialView", model);
        }

        public PartialViewResult GetModelModal()
        {
            unitOfWork = new EFUnitOfWork(db);

            List<ModelModel> model = new List<ModelModel>();

            model = unitOfWork.GetRepository<Model>().GetAll().ProjectTo<ModelModel>().ToList();

            return PartialView("_ModelPartialView", model);
        }
        #endregion

        #region GetList

        public PartialViewResult GetTypeList()
        {
            unitOfWork = new EFUnitOfWork(db);

            List<TypeModel> model = new List<TypeModel>();

            model = unitOfWork.GetRepository<Type>().GetAll().ProjectTo<TypeModel>().ToList();
            
            unitOfWork.Dispose();

            return PartialView("_TypeListPartialView",model);

        }

        public PartialViewResult GetBrandList()
        {
            unitOfWork = new EFUnitOfWork(db);

            List<BrandModel> model = new List<BrandModel>();

            model = unitOfWork.GetRepository<Brand>().GetAll().ProjectTo<BrandModel>().ToList();

            unitOfWork.Dispose();

            return PartialView("_BrandListPartialView", model);
        }

        public PartialViewResult GetModelList()
        {
            unitOfWork = new EFUnitOfWork(db);

            List<ModelModel> model = new List<ModelModel>();

            model = unitOfWork.GetRepository<Model>().GetAll().ProjectTo<ModelModel>().ToList();

            unitOfWork.Dispose();

            return PartialView("_ModelListPartialView", model);
        }

        public PartialViewResult GetAddType()
        {
            return PartialView("_TypeAddPartialView" ,new TypeModel());
        }

        public PartialViewResult GetAddBrand()
        {
            unitOfWork = new EFUnitOfWork(db);
            
            BrandEditViewModel bevm = new BrandEditViewModel();

            bevm.BrandModel = new BrandModel();

            var types = unitOfWork.GetRepository<Type>().GetAll().Select(t => new SelectListItem()
            {
                Value = t.Id.ToString(),
                Text = t.Name
            }).ToList();

            bevm.typeIds = new List<int>();

            bevm.Types = new MultiSelectList(types,"Value","Text",bevm.typeIds);
            
            

            unitOfWork.Dispose();
            return PartialView("_BrandAddPartialView",bevm);
        }

        public PartialViewResult GetAddModel()
        {
            unitOfWork = new EFUnitOfWork(db);
            
            ModelEditViewModel mevm = new ModelEditViewModel();

            mevm.ModelModel = new ModelModel();

            var brands = unitOfWork.GetRepository<Brand>().GetAll().Select(t => new SelectListItem()
            {
                Value = t.Id.ToString(),
                Text = t.Name
            }).ToList();

            mevm.Brands = new SelectList(brands,"Value","Text",mevm.brandId);
            
            

            unitOfWork.Dispose();
            return PartialView("_ModelAddPartialView",mevm);
        }

        #endregion

        #region GetEdit

        [HttpGet]
        public PartialViewResult EditType(int id)
        {
            unitOfWork = new EFUnitOfWork(db);

            var model = unitOfWork.GetRepository<Type>().GetById(id);


            TypeModel tm = Mapper.Map<Type, TypeModel>(model);

            unitOfWork.Dispose();

            return PartialView("_TypeEditPartialView", tm);
        }
        [HttpGet]
        public PartialViewResult EditBrand(int id)
        {
            unitOfWork = new EFUnitOfWork(db);

            BrandEditViewModel bevm = new BrandEditViewModel();

            var model = unitOfWork.GetRepository<Brand>().GetById(id);

            BrandModel bm = Mapper.Map<Brand, BrandModel>(model);

            var types = unitOfWork.GetRepository<Type>().GetAll().Select(t => new SelectListItem()
            {
                Value = t.Id.ToString(),
                Text = t.Name
            }).ToList();

            bevm.typeIds = model.TypeBrands.Select(tb => tb.TypeId).ToList();

            bevm.BrandModel = bm;

            bevm.Types = new MultiSelectList(types,"Value","Text",bevm.typeIds);

            unitOfWork.Dispose();

            return PartialView("_BrandEditPartialView",bevm);
        }
        [HttpGet]
        public PartialViewResult EditModel(int id)
        {
            unitOfWork = new EFUnitOfWork(db);

            ModelEditViewModel mevm = new ModelEditViewModel();
            
            var model = unitOfWork.GetRepository<Model>().GetById(id);

            ModelModel mm = Mapper.Map<Model, ModelModel>(model);

            var brands = unitOfWork.GetRepository<Brand>().GetAll().Select(t => new SelectListItem()
            {
                Value = t.Id.ToString(),
                Text = t.Name
            }).ToList();

            mevm.brandId = model.BrandId;

            mevm.ModelModel = mm;

            mevm.Brands = new SelectList(brands,"Value","Text",mevm.brandId);

            unitOfWork.Dispose();

            return PartialView("_ModelEditPartialView",mevm);
        }


        #endregion

        #region Operation

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTypeConfirm(TypeModel tm)
        {
            unitOfWork = new EFUnitOfWork(db);

            var entity = Mapper.Map<TypeModel, Type>(tm);

            unitOfWork.GetRepository<Type>().Update(entity);

            unitOfWork.SaveChanges();

            unitOfWork.Dispose();


            return RedirectToAction("GetTypeList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBrandConfirm(BrandEditViewModel bevm,HttpPostedFileBase file1)
        {
            unitOfWork = new EFUnitOfWork(db);

            

            var entity = unitOfWork.GetRepository<Brand>().GetById(bevm.BrandModel.Id);

            entity.Name = bevm.BrandModel.Name;

            if (file1 != null)
            {
                string fullPath = Request.MapPath(entity.BrandImgUrl);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                entity.BrandImgUrl = "~/Images/BrandImages/" + bevm.BrandModel.Name + ".png";;
                string path = Path.Combine(Server.MapPath(entity.BrandImgUrl));
                file1.SaveAs(path);
            }

            var typeBrands = unitOfWork.GetRepository<TypeBrand>().GetAll()
                .Where(tb => tb.BrandId == bevm.BrandModel.Id).ToList();

            foreach (var typeBrand in typeBrands)
            {
                unitOfWork.GetRepository<TypeBrand>().Delete(typeBrand);
            }


            entity.TypeBrands = bevm.typeIds.Select(t => new TypeBrand()
            {
                TypeId = t,
                BrandId = entity.Id
            }).ToList();


            unitOfWork.GetRepository<Brand>().Update(entity);

            unitOfWork.SaveChanges();
                
            unitOfWork.Dispose();


            return RedirectToAction("GetBrandList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditModelConfirm(ModelEditViewModel mevm)
        {
            unitOfWork = new EFUnitOfWork(db);

            var entity = unitOfWork.GetRepository<Model>().GetById(mevm.ModelModel.Id);

            List<Vehicle> vehicles = unitOfWork.GetRepository<Vehicle>().GetAll(v => v.BrandId == entity.BrandId).ToList();

            entity.Name = mevm.ModelModel.Name;

            entity.BrandId = mevm.brandId;

            unitOfWork.GetRepository<Model>().Update(entity);

            foreach (var vehicle in vehicles)
            {
                vehicle.BrandId = mevm.brandId;
                
                unitOfWork.GetRepository<Vehicle>().Update(vehicle);
            }



            unitOfWork.SaveChanges();
                
            unitOfWork.Dispose();


            return RedirectToAction("GetModelList");
        }



        [HttpPost]
        public ActionResult DeleteType(int id)
        {
            unitOfWork = new EFUnitOfWork(db);

            unitOfWork.GetRepository<Type>().Delete(id);

            List<TypeBrand> tbList = unitOfWork.GetRepository<TypeBrand>().GetAll(tb => tb.TypeId == id).ToList();

            foreach (var tb in tbList)
            {
                unitOfWork.GetRepository<TypeBrand>().Delete(tb);
            }

            unitOfWork.SaveChanges();

            unitOfWork.Dispose();

            return RedirectToAction("GetTypeList");
            
        }

        [HttpPost]
        public ActionResult DeleteBrand(int id)
        {
            unitOfWork = new EFUnitOfWork(db);

            var BrandImgUrl = unitOfWork.GetRepository<Brand>().GetById(id).BrandImgUrl;

            unitOfWork.GetRepository<Brand>().Delete(id);

            List<TypeBrand> tbList = unitOfWork.GetRepository<TypeBrand>().GetAll(tb => tb.BrandId == id).ToList();

            List<Model> mList = unitOfWork.GetRepository<Model>().GetAll(m => m.BrandId == id).ToList();

            List<Vehicle> vehicles = unitOfWork.GetRepository<Vehicle>().GetAll(v => v.BrandId == id).ToList();

            foreach (var m in mList)
            {
                unitOfWork.GetRepository<Model>().Delete(m);
            }

            foreach (var vehicle in vehicles)
            {
                unitOfWork.GetRepository<Vehicle>().Delete(vehicle);
            }

            foreach (var tb in tbList)
            {
                unitOfWork.GetRepository<TypeBrand>().Delete(tb);
            }

            unitOfWork.SaveChanges();

            string fullPath = Request.MapPath(BrandImgUrl);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            unitOfWork.Dispose();

            return RedirectToAction("GetBrandList");
            
        }

        [HttpPost]
        public ActionResult DeleteModel(int id)
        {
            unitOfWork = new EFUnitOfWork(db);

            unitOfWork.GetRepository<Model>().Delete(id);

            List<Vehicle> vehicles = unitOfWork.GetRepository<Vehicle>().GetAll(v => v.ModelId == id).ToList();

            foreach (var vehicle in vehicles)
            {
                unitOfWork.GetRepository<Vehicle>().Delete(vehicle);
            }

            unitOfWork.SaveChanges();

            unitOfWork.Dispose();

            return RedirectToAction("GetModelList");
            
        }



        [HttpPost]
        public ActionResult AddType(TypeModel tm)
        {
            unitOfWork = new EFUnitOfWork(db);
            
            var entity = Mapper.Map<TypeModel, Type>(tm);

            unitOfWork.GetRepository<Type>().Add(entity);

            unitOfWork.SaveChanges();

            unitOfWork.Dispose();

            return RedirectToAction("GetTypeList");
        }

        [HttpPost]
        public ActionResult AddBrand(BrandEditViewModel bevm,HttpPostedFileBase file1)
        {
            unitOfWork = new EFUnitOfWork(db);
            
            var entity = Mapper.Map<BrandModel, Brand>(bevm.BrandModel);

            if (file1 != null)
            {
                entity.BrandImgUrl = "~/Images/BrandImages/" + bevm.BrandModel.Name + ".png";;
                string path = Path.Combine(Server.MapPath(entity.BrandImgUrl));
                file1.SaveAs(path);
            }

            entity.TypeBrands = bevm.typeIds.Select(t => new TypeBrand()
            {
                TypeId = t,
                BrandId = entity.Id
            }).ToList();


            unitOfWork.GetRepository<Brand>().Add(entity);

            unitOfWork.SaveChanges();

            unitOfWork.Dispose();

            return RedirectToAction("GetBrandList");
        }

        [HttpPost]
        public ActionResult AddModel(ModelEditViewModel mevm)
        {
            unitOfWork = new EFUnitOfWork(db);
            
            var entity = Mapper.Map<ModelModel, Model>(mevm.ModelModel);

            entity.BrandId = mevm.brandId;

            unitOfWork.GetRepository<Model>().Add(entity);

            unitOfWork.SaveChanges();

            unitOfWork.Dispose();

            return RedirectToAction("GetModelList");
        }

        #endregion
        
    }
}