using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car_Galery.Entities
{
    public class TypeBrand
    {
        public int Id { get; set; }

        public int BrandId { get; set; }

        public int TypeId { get; set; }

        public virtual Type Type { get; set; }

        public virtual Brand Brand { get; set; }
    }
}