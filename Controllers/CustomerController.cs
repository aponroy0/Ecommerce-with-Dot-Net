using Ecommerce.Auth;
using Ecommerce.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        EcommEntities4 db = new EcommEntities4();

        [Logged]
        public ActionResult Index()
        {
            var user = (User)Session["user"];
            var orders = (from o in db.Orders
                          where o.CustomerId == user.CustomerId
                          select o).ToList();
            return View(orders);
        }


    }
}