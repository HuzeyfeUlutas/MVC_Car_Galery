using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Car_Galery.Models.ViewModels
{
    public class VehicleOperationView
    {
        public SelectList Brands { get; set; }

        public SelectList Models { get; set; }

        public SelectList Types { get; set; }
        

        public VehicleModalViewModel VehicleModalViewModel { get; set; }
    }
}