using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Ecommerce.Auth
{
    public class Admin : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["user"]!= null && httpContext.Session["userRole"].ToString() == "Admin" )
            {
                return true;
            }
            return false;
        }
    }
    public class Logged : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["user"]!= null && httpContext.Session["userRole"].ToString() == "Customer" )
            {
                return true;
            }
            return false;
        }
    }
}