using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Car_Galery.Entities
{
    public class Model
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int BrandId { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual List<Vehicle> Vehicles { get; set; }
    }
}