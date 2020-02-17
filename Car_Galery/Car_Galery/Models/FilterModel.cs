using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car_Galery.Models
{
    public class FilterModel
    {
        public string TypeName { get; set; }

        public string BrandName { get; set; }

        public string ModelName { get; set; }

        public string FuelType { get; set; }

        public string Transmission { get; set; }

        public string Year { get; set; }

        public string MinPrice { get; set; }

        public string MaxPrice { get; set; }

        public string MinKm { get; set; }

        public string MaxKm { get; set; }



    }
}