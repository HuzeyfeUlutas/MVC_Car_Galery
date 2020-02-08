using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Car_Galery.Entities
{
    public class Type
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual List<TypeBrand> TypeBrands { get; set; }

        public virtual List<Vehicle> Vehicles { get; set; }

    }
}