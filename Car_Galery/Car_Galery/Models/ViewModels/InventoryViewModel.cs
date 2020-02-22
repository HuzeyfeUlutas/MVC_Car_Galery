using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace Car_Galery.Models.ViewModels
{
    public class InventoryViewModel
    {

        public IPagedList<VehicleModel> PagedVehicleModels { get; set; }

        public List<BrandModelsModel> BrandModelModels { get; set; }

        public FilterModel FilterModel { get; set; }

        public List<TypeModel> TypeModels { get; set; }

    }
}