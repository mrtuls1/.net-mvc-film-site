using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcVize.Settings
{
    public class _SecurityFilter :ActionFilterAttribute
    {
        //action çalışmadan öncesi
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //hiç bir işlem yapmak istemiyorsan böyle bırak
            // base.OnActionExecuted(filterContext);

            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            if(HttpContext.Current.Session["Kullanici"]==null &&((controllerName !="Login")&& (controllerName !="Register")))
            {
                //session yoksa yönlendirme yapılır
                filterContext.Result = new RedirectResult("/Login/Index");
                return;
            }
            base.OnActionExecuted(filterContext);
        }
    }
}