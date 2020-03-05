using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Car_Galery.Context;
using Car_Galery.Entities;
using Car_Galery.Managers;
using Car_Galery.Managers.Abstract;
using Car_Galery.Models;

namespace Car_Galery.Controllers
{
    public class HomeController : Controller
    {

        private  DbContext db = new VehiclesContext();
        private IUnitOfWork unitOfWork;
        public ActionResult Index()
        {
            unitOfWork = new EFUnitOfWork(db);

            List<BrandModel> bm = new List<BrandModel>();

            bm = unitOfWork.GetRepository<Brand>().GetAll().ProjectTo<BrandModel>().ToList();

            unitOfWork.Dispose();
            return View(bm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

    }
}