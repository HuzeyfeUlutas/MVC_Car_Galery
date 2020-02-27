using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Car_Galery.Context;
using Car_Galery.Managers;
using Car_Galery.Managers.Abstract;
using Car_Galery.Models;
using Type = Car_Galery.Entities.Type;

namespace Car_Galery.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminOperationController : Controller
    {

        private IUnitOfWork unitOfWork;
        private DbContext db = new VehiclesContext();
        // GET: AdminOperation
        public PartialViewResult GetTypeModal()
        {
            unitOfWork = new EFUnitOfWork(db);

            List<TypeModel> model = new List<TypeModel>();

            model = unitOfWork.GetRepository<Type>().GetAll().ProjectTo<TypeModel>().ToList();



            return PartialView("_TypePartialView");
        }

        //[HttpGet]
        //public PartialViewResult EditType(int id)
        //{

        //    return PartialView("")
        //}

        //[HttpPost]
        //[NonAction]
        //public PartialViewResult EditTypeConfirm(int id)
        //{
        //    return 
        //}
    }
}