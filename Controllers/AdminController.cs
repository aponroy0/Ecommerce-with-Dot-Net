using Ecommerce.Auth;
using Ecommerce.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class AdminController : Controller
    {

        EcommEntities4 db = new EcommEntities4();
        [Admin]
        [HttpGet]
        public ActionResult Dashboard()
        {
            var PlaceedOrder = db.Orders;
            return View(PlaceedOrder);
        }


    }
}