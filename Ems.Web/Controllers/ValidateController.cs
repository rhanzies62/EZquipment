using Ems.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ems.Web.Controllers
{
    public class ValidateController : Controller
    {
        private readonly ICompanyService _companyService;
        public ValidateController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // GET: Validate
        public ActionResult Index(string t)
        {
            var response = _companyService.ValidateEmail(t);
            return View(response);
        }
    }
}