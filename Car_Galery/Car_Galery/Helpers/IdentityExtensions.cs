using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Car_Galery.Helpers
{
    public static class IdentityExtensions
    {
        public static string GetBalance(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity) identity).FindFirst("Balance");

            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}