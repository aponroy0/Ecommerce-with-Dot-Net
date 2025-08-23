using AutoMapper;
using Ecommerce.DTO;
using Ecommerce.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Auth;
using Antlr.Runtime;
using System.Web.UI;

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
            {     //Unboxing the cart
                  cart = (List<ProductDTO>)Session["cart"];
            }

            // If products exist in the cart...
            var exist = (from pro in cart where pro.Id == id select pro).SingleOrDefault();
            if(exist != null)
            {
                exist.Qty++;
            }
            else
            {
                cart.Add(pr);
            }
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

// Place Order
       public ActionResult PlaceOrder(decimal gtotal)
        {
            var user = (User)Session["user"];
            var cart = (List<ProductDTO>)Session["cart"];

            var od = new Order()
            {
                Date = DateTime.Now,
                Total = gtotal,
                CustomerId = (int)user.CustomerId,
                StatusId = 1,
            };

            db.Orders.Add(od);
            db.SaveChanges();

            foreach (var item in cart)
            {
                var odtal = new OrderDetail()
                {
                    PId = item.Id,
                    Qty = item.Qty,
                    Price = item.Price,
                    OId = od.Id,
                };
                db.OrderDetails.Add(odtal);
            }

            db.SaveChanges();
            TempData["msg"] = "Order Placed Successfully";
            TempData["cart"] = null;
            return RedirectToAction("Index");

        }

        // Incresing part of the product

        public ActionResult Increase(int id)
        {
            var cart = (List<ProductDTO>)Session["cart"];
            var pq = (from p in cart
                      where p.Id == id
                      select p).SingleOrDefault();
            pq.Qty++;
            return RedirectToAction("Cart");
        }

        // Decrease an item
        public ActionResult Decrease (int id)
        {
            var cart = (List<ProductDTO>)Session["cart"];
            var pq = (from p in cart
                      where p.Id == id
                      select p).SingleOrDefault();
            pq.Qty--;
            return RedirectToAction("cart");
        }

        // Clear an item from the cart

        public ActionResult Clear (int id)
        {
            var cart = (List<ProductDTO>)Session["cart"];
            var cl = (from c in cart
                      where c.Id == id
                      select c).SingleOrDefault();
            cart.Remove(cl);
  
            if ( cart.Count == 0)
            {
                return RedirectToAction("Index");
            }
            Session["cart"] = cart;
            return RedirectToAction("cart");
        }




    }




}