using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Car_Galery.Models.ViewModels
{
    public class BrandEditViewModel
    {
        public BrandModel BrandModel { get; set; }

        public MultiSelectList Types { get; set; }

        public List<int> typeIds { get; set; }

    }
}