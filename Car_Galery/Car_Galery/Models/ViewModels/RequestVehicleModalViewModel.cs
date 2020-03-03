using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car_Galery.Models.ViewModels
{
    public class RequestVehicleModalViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Year { get; set; }

        public int Km { get; set; }

        public string Color { get; set; }

        public int Price { get; set; }

        public string Fuel { get; set; }

        public string Transmission { get; set; }

        public bool Rentable { get; set; }

        public string BrandName { get; set; }

        public string ModelName { get; set; }

        public string TypeName { get; set; }

        public int BrandId { get; set; }

        public int ModelId { get; set; }

        public int TypeId { get; set; }

        public DateTime RequestTime { get; set; }
    }
}