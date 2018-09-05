using Ems.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ems.Web.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly IUserService _userService;
        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetProfile()
        {
            var user = _userService.Get(appUser.UserId);
            return Json(user, JsonRequestBehavior.AllowGet);
        }

    }
}