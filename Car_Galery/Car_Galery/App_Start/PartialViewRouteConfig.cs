using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Car_Galery.App_Start
{
    public class PartialViewRouteConfig : RazorViewEngine
    {
        private static readonly string[] NewPartialViewFormats =
        {
            "~/Views/Shared/AdminOperationPartial/{0}.cshtml",
            "~/Views/Shared/InventoryPartial/{0}.cshtml",
            "~/Views/Shared/AdminOperationPartial/Type/{0}.cshtml",
            "~/Views/Shared/AdminOperationPartial/Brand/{0}.cshtml",
            "~/Views/Shared/AdminOperationPartial/Model/{0}.cshtml"
        };

        public PartialViewRouteConfig()
        {
            base.PartialViewLocationFormats = base.PartialViewLocationFormats.Union(NewPartialViewFormats).ToArray();
        }
    };
}