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

        [HttpGet]
        public ActionResult Confirm(int id)
        {
            var Order = (from od in db.Orders
                                where od.Id == id
                                select od).SingleOrDefault();
            var Details = Order.OrderDetails.ToList();
            var pr = (from p in db.Products
                      where p.Id == id
                      select p).SingleOrDefault();

            foreach (var item in Details)
            {
                if( item.Qty <= pr.Qty && item.PId == pr.Id)
                {
                    pr.Qty = pr.Qty - item.Qty;
                

                }
                else
                {
                    TempData["Msg"] = $"Not enough stock for {pr.Name}. Need {item.Qty}, have {pr.Qty}.";
                    TempData["class"] = "danger";
                    return RedirectToAction("Dashboard"); 
                }

            }
            if(Order.StatusId== 0){
                Order.StatusId = 2;
                db.SaveChanges();
            }

            db.SaveChanges();

            TempData["Msg"] = "Order confirmed.";
            TempData["class"] = "danger";
            return RedirectToAction("Dashboard"); 




        }
        

    }
}