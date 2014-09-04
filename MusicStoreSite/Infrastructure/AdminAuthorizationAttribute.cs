using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MusicStoreSite.Infrastructure
{
    public class AdminAuthorizationAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary 
                {
                    { "action", "AccessDenied" },
                    { "controller", "Store" },
                    { "area", "" }
                });
        }
    }
}