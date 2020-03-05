using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using Car_Galery.Models;
using Microsoft.AspNet.Identity;

namespace Car_Galery.Helpers
{
    public static  class IdentityExtensions
    {
        

        public static string GetBalance(this IIdentity identity)

        {

            ApplicationDbContext UsersContext = new ApplicationDbContext();

            var Balance = UsersContext.Users.Find(identity.GetUserId()).Balance;

            ((ClaimsIdentity) identity).RemoveClaim(((ClaimsIdentity) identity).FindFirst("Balance"));

            ((ClaimsIdentity) identity).AddClaim(new Claim("Balance", Balance.ToString()));

            var claim = ((ClaimsIdentity) identity).FindFirst("Balance");


            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}