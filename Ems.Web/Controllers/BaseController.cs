using Ems.Web.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ems.Web.Controllers
{
    [EmsAuthorize]
    public class BaseController : Controller
    {
        // GET: Base
        public AppPrincipal appUser
        {
            get { return (AppPrincipal)User; }
        }
    }
}