using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UoW.ADMIN.Helpers
{
    public class CheckRole : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.GetString("admin") == null)
            {
                filterContext.HttpContext.Response.Redirect("/Home/Error");
            }
            //base.OnActionExecuted(filterContext);
        }     
    }

    public class RoleAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private string[] Permissions;
        public RoleAuthorizeAttribute(params string[] permissions)
        {
            Permissions = permissions;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            //gelen yetkileri kontrol et
            context.Result = new ForbidResult();
        }
    }
}
