using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace iem_website.Controllers
{
    public class NavigationController : SurfaceController
    {
        public ActionResult MainNavigation()
        {
            string test = "2";
            return View("~/Views/Partials/Test.cshtml");
        }
    }
}