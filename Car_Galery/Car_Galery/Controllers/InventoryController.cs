using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Car_Galery.Controllers
{
    public class InventoryController : Controller
    {


        // GET: Inventory
        public ActionResult Index()
        {
            return View();
        }
    }
}