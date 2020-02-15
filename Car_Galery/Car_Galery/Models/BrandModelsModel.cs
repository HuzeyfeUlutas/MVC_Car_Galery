using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car_Galery.Models
{
    public class BrandModelsModel
    {
        public int BrandId { get; set; }

        public string BrandName { get; set; }

        public List<ModelModel> brandModels { get; set; }
    }
}