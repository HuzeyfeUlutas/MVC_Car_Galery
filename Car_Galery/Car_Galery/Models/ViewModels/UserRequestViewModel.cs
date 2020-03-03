using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace Car_Galery.Models.ViewModels
{
    public class UserRequestViewModel
    {
        public IPagedList<UserRequestModel> PagedListUsers { get; set; }

        public int UsersRequestCount { get; set; }
    }
}