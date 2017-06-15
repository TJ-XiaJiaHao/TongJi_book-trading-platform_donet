using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace net.BusinessLayer
{
    public class AdminFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (filterContext.HttpContext.Session["userName"] == null || (string)filterContext.HttpContext.Session["userRole"] != "Admin")
            {
                filterContext.Result = new RedirectResult("/User/Login");
            }
        }
    }
    public class loginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (filterContext.HttpContext.Session["userName"] == null)
            {
                filterContext.Result = new RedirectResult("~/User/Login");
            }
        }
    }
}