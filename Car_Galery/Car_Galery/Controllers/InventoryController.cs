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
        VehiclesContext db = new VehiclesContext();
        IManager a;

        // GET: Inventory
        public ActionResult Index()
        {

            IManager a = new EFManager(db);


            a.GetRepository<Vehicle>().GetAll(x => x.Km == 200000);


            a.Dispose();
            return View();
        }
    }
}