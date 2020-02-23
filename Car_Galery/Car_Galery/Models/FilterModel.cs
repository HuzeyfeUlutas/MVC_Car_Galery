using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car_Galery.Models
{
    public class FilterModel
    {
        public bool Filtered { get; set; }

        public int? TypeId { get; set; }

        public string FuelType { get; set; }

        public string Transmission { get; set; }

        public string Year { get; set; }

        public int? MinPrice { get; set; }

        public int? MaxPrice { get; set; }

        public int? MinKm { get; set; }

        public int? MaxKm { get; set; }

        public int? BrandId { get; set; }

        public int? ModelId { get; set; }

        public string SortBy { get; set; }

        public string SearchText { get; set; }

        public int? PageNumber { get; set; }

        public int? ResultCount { get; set; }

    }
}