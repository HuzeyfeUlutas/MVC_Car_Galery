using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Car_Galery.Context;
using Car_Galery.Entities;
using Car_Galery.Managers;
using Car_Galery.Managers.Abstract;

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


            
            return View();
        }
    }
}