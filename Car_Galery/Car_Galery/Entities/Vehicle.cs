using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Car_Galery.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        [StringLength(4)]
        public string Year { get; set; }

        public int Km { get; set; }

        public string Color { get; set; }

        public int Price { get; set; }

        public string Fuel { get; set; }

        public string Transmission { get; set; }

        public bool Rentable { get; set; }

        public int BrandId { get; set; }

        public int ModelId { get; set; }

        public int TypeId { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual Model Model { get; set; }

        public virtual Type Type { get; set; }



    }
}