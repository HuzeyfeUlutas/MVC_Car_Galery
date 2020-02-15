using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Car_Galery.Models
{
    public class TypeModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}