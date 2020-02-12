using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Car_Galery.Entities;

namespace Car_Galery.Models
{
    public class ExampleViewModel
    {
        public List<Vehicle> Vehicles { get; set; }

        public string SelectedVAlue { get; set; }


    }
}