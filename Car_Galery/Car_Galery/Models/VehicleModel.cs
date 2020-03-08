using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Car_Galery.Models
{
    public class VehicleModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Vehicle Name")]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Enter Vehicle Year")]
        [StringLength(4)]
        public string Year { get; set; }

        [Required(ErrorMessage = "Enter Vehicle Km")]
        public int Km { get; set; }

        [Required(ErrorMessage = "Enter Vehicle Color")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Enter Vehicle Price")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Enter Vehicle Fuel")]
        public string Fuel { get; set; }

        [Required(ErrorMessage = "Enter Vehicle Transmission")]
        public string Transmission { get; set; }

        [Required(ErrorMessage = "Enter Vehicle Rentable Value")]
        public bool Rentable { get; set; }

        public bool Rented { get; set; }

        [Required(ErrorMessage = "Enter Vehicle BrandId")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Enter Vehicle ModelId")]
        public int ModelId { get; set; }

        [Required(ErrorMessage = "Enter Vehicle TypeId")]
        public int TypeId { get; set; }
    }
}