using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Car_Galery.Entities;
using Car_Galery.Models;

namespace Car_Galery.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(ExampleViewModel ba)
        {

                List<Vehicle> c = new List<Vehicle>();

                c = ba.Vehicles;

                string x = ba.SelectedVAlue;



            return View(ba);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public ActionResult About(ExampleViewModel a)
        {
            if (a.SelectedVAlue == "merhaba")
            {
                a.Vehicles = new List<Vehicle>();
                a.Vehicles.Add(new Vehicle()
                {
                    Name = "Mercedes"
                });
            }


            return RedirectToAction("Index", "Home", a);
        }

    }
}