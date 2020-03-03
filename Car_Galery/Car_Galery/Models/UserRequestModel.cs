using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car_Galery.Models
{
    public class UserRequestModel
    {
        public int Id { get; set; }

        public string Location { get; set; }

        public string UserName { get; set; }

        public string UserPhoneNumber { get; set; }

        public int VehicleId { get; set; }

        public DateTime RequestDateTime { get; set; }
    }
}