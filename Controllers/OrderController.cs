using AutoMapper;
using Ecommerce.DTO;
using Ecommerce.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Auth;

namespace EComm.Controllers
{
    public class OrderController : Controller
    {
        // 
        EcommEntities4 db = new EcommEntities4();

        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Product, ProductDTO>().ReverseMap();
            });
            return new Mapper(config);
        }

        [Logged]
        public ActionResult Index()
        {
            var data = db.Products.ToList();

            var products = GetMapper().Map<List<ProductDTO>>(data);
            return View(products);
        }
        public ActionResult AddtoCart(int id)
        {
            var p = db.Products.Find(id);
            var pr = GetMapper().Map<ProductDTO>(p);

            List<ProductDTO> cart = null;
            pr.Qty = 1;
            if (Session["cart"] == null)
            {
                cart = new List<ProductDTO>();
            }
            else
            {
                cart = (List<ProductDTO>)Session["cart"];
            }
            cart.Add(pr);
            Session["cart"] = cart;
            return RedirectToAction("Index");

        }
        public ActionResult Cart()
        {
            var cart = (List<ProductDTO>)Session["cart"];
            return View(cart);
        }


// This scope of code is for cencel the order
        [Logged]
        public ActionResult CancelByUser(int id)
        {
            var od = db.Orders.Find(id);
            od.StatusId = 3;
            db.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }


// This scope of code to show the details of orders.
// Orders is list of orders. We find the specific order using customerID....

        [Logged]
        public ActionResult Details(int id)
        {
            var od = db.Orders.Find(id);
            return View(od);
        }
    }
}