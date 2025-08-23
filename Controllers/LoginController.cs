using Ecommerce.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class LoginController : Controller
    {
        EcommEntities4 db = new EcommEntities4();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Index(string email, string password)
        {
            // Here, we are cheking if the value exist in the database for authentication
            var user = (from u in db.Users
                        where
                        u.Email.Equals(email) &&
                        u.Password.Equals(password)
                        select u).SingleOrDefault();


            
            if (user != null)
            {

                Session["user"] = user;
                Session["userRole"] = user.Type; // "Admin" or "User" 

                // Redirect based on user type
                if (user.Type == "Admin")
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                else if (user.Type == "Customer")
                {
                    return RedirectToAction("Index", "Customer");
                }
            }

            TempData["msg"] = "Invalid username and password!";
            TempData["class"] = "danger";
            return View();

         }
    }
}