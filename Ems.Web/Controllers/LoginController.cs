using Ems.Domain.Common;
using Ems.Domain.DTO;
using Ems.Web.Authentication;
using Ems.Web.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Ems.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult loginform()
        {
            return View();
        }
        [HttpPost, EmsValidateAntiForgeryToken]
        public JsonResult Login(UserAccountDto loginView, string ReturnUrl = "")
        {
            var loginResult = Membership.ValidateUser(loginView.UserName, loginView.Password);
            var user = (EmsMembershipUser)Membership.GetUser(loginView.UserName, false);
            Response<LoginInfoDto> response = user.Response;
            if (loginResult)
            {
                LoginInfoDto userModel = new LoginInfoDto()
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmailAddress = user.Email
                };

                string userData = JsonConvert.SerializeObject(userModel);
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                    (
                    1, loginView.UserName, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData
                    );

                string enTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie("Cookie1", enTicket);
                Response.Cookies.Add(faCookie);
            }
            return Json(response);
        }
    }
}