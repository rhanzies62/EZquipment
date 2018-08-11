using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ems.Web.Authentication
{
    public class EmsAuthorizeAttribute : AuthorizeAttribute
    {
        protected virtual AppPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as AppPrincipal; }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return CurrentUser == null ? false : true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            RedirectToRouteResult routeData = null;

            if (CurrentUser == null)
            {
                routeData = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(
                    new
                    {
                        controller = "login",
                        action = "index",
                        ReturnUrl = "dashboard/index"
                    }));
            }
            else
            {
                routeData = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(
                    new {
                     controller = "Error",
                     action = "AccessDenied"
                    }));
            }

            filterContext.Result = routeData;
        }
    }

    public class EmsAuthAttribute : AuthorizeAttribute
    {
        protected virtual AppPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as AppPrincipal; }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return CurrentUser != null ? false : true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            RedirectToRouteResult routeData = null;
            
            if (CurrentUser != null)
            {
                routeData = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(
                    new
                    {
                        controller = "dashboard",
                        action = "index",
                    }));
            }

            filterContext.Result = routeData;
        }

    }

    public class NoCacheAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
            filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.SetNoStore();

            base.OnResultExecuting(filterContext);
        }
    }
}