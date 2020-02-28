using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
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

        public PartialViewResult GetAddType()
        {
            return PartialView("_TypeAddPartialView" ,new TypeModel());
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

            unitOfWork.GetRepository<Brand>().Delete(id);

            List<TypeBrand> tbList = unitOfWork.GetRepository<TypeBrand>().GetAll(tb => tb.BrandId == id).ToList();

            foreach (var tb in tbList)
            {
                unitOfWork.GetRepository<TypeBrand>().Delete(tb);
            }

            unitOfWork.SaveChanges();

            unitOfWork.Dispose();

            return RedirectToAction("GetTypeList");
            
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

        #endregion
        
    }
}