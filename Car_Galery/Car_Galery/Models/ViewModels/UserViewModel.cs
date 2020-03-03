using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace Car_Galery.Models.ViewModels
{
    public class UserViewModel
    {
        public IPagedList<User> PagedListUsers { get; set; }

        public int UsersCount { get; set; }

    }
}