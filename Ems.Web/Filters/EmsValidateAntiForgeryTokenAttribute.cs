using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Helpers;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace Ems.Web.Filters
{
    public sealed class EmsValidateAntiForgeryTokenAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            var context = filterContext.HttpContext;
            var headers = context.Request.Headers;
            var tokenHeader = headers["X-XSRF-Token"];
            var tokenCookie = context.Request.Cookies[AntiForgeryConfig.CookieName].Value;

            AntiForgery.Validate(tokenCookie != null ? tokenCookie : null, tokenHeader);

            base.OnActionExecuting(filterContext);
        }
    }
}