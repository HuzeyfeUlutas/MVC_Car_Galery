using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Car_Galery.Models.ViewModels
{
    public class ModelEditViewModel
    {
        public ModelModel ModelModel { get; set; }

        public SelectList Brands { get; set; }

        public int brandId { get; set; }

    }
}