﻿using Ems.Services.Interface;
using Ems.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ems.Web.ExtensionHelper;
using Ems.Services;

namespace Ems.Web.Controllers
{
    public partial class HomeController : Controller
    {
        private readonly ICompanyService _companyService;
        public HomeController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CompanyRegister(CompanyRegistrationViewModel model)
        {
            var companyDto = model.MapToCompanyDto();
            var response = _companyService.CreateCompanyWithUser(companyDto,EmsResousrce.DefaultCreatedBy);
            return Json(response);
        }

        public virtual ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public virtual ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}