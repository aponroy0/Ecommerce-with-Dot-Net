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
        EcommEntities1 db = new EcommEntities1();

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
    }
}