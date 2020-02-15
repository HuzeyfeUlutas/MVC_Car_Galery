using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car_Galery.Models.ViewModels
{
    public class InventoryViewModel
    {
        public List<VehicleModel> VehicleModels { get; set; }

        public List<TypeModel> TypeModels { get; set; }

        public List<BrandModelsModel> BrandModelModels { get; set; }
    }
}