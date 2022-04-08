using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UoW.ADMIN.Helpers
{
    public class CheckOtherLogin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.GetString("userB") == null)
            {
                filterContext.Result = new RedirectResult("/Bayi/SellerLogin");
            }
            //base.OnActionExecuted(filterContext);
        }
    }
}
