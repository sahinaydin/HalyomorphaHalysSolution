using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HalyomorphaHalys.WebApp.Business
{
    public class AuthAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!SessionHelper.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("/Login/Index");
            }
        }
    }
}