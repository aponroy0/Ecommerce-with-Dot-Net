using Ecommerce.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;

public class AccountController : Controller
{
    private EcommEntities4 db = new EcommEntities4();

    // GET: SignUp Page
    public ActionResult SignUp()
    {
        return View();
    }

    // POST: Handle Form Submission
    [HttpPost]
    public ActionResult SignUp(SignUpViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Step 1: Save Customer
            var customer = new Customer
            {
                Name = model.Name,
                Email = model.Email,
                Username = model.Username,
                Phone = model.Phone
            };
            db.Customers.Add(customer);
            db.SaveChanges();

            // Step 2: Save User linked to Customer
            var user = new User
            {
                Username = model.Username,
                Password = model.Password,  // ⚠️ hash before saving
                Email = model.Email,
                Type = model.Type,
                CustomerId = customer.Id
            };
            db.Users.Add(user);
            db.SaveChanges();

            return RedirectToAction("Login"); // after signup go to login
        }

        return View(model);
    }
}
