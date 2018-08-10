using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ems.Domain.DTO;
using System.Collections.Generic;
using Ems.Services;
using Ems.Data;
using Ems.Domain;
using Ems.Services.Config;
using Ems.ExternalServices;
using System.Linq;
namespace Ems.Service.Test
{
    [TestClass]
    public class CompanyServiceTest
    {
        public CompanyServiceTest()
        {
        }

        #region company dto
        CompanyDto company = new CompanyDto
        {
            Name = $"Basecamp {Guid.NewGuid().ToString("N").Substring(0, 10)}",
            Founded = DateTime.UtcNow.Year,
            Background = "Test",
            Address = new AddressDto
            {
                City = "San Juan",
                Long = 1,
                Lat = 2,
                PostalCode = "1422",
                StreetAddress = "1110 wilshire annapolis"
            },
            Users = new List<UserDto>
                {
                    new UserDto
                            {
                                CompanyId = 1,
                                Address = new AddressDto
                                {
                                    City = "San Juan",
                                    Long = 1,
                                    Lat = 2,
                                    PostalCode = "1422",
                                    StreetAddress = "1110 wilshire annapolis"
                                },
                                FirstName = "Francisco",
                                LastName = "Cebu",
                                MiddleName = Guid.NewGuid().ToString("N").Substring(0,10),
                                UserAccount = new UserAccountDto
                                {
                                    Password = "1234567",
                                    UserName = Guid.NewGuid().ToString("N").Substring(0, 10),
                                    IsActive = true,
                                    IsLock = false
                                },
                                EmailAddress = $"fcebu{Guid.NewGuid().ToString("N").Substring(0, 10)}@gmail.com",

                            }
                }
        };
        #endregion
        [TestMethod]
        public void CreateCompanyWithUser_ExistingCompanyName()
        {
            AutoMapperConfig.Init();
            var companyService = new CompanyService(new UnitOfWork(new EmsDevEntities()),new EmailService());
            company.Name = "Basecamp";
            var response = companyService.CreateCompanyWithUser(company, "test");
            Assert.AreEqual(false, response.IsSuccess);
        }

        [TestMethod]
        public void CreateCompanyWithUser_Success()
        {
            var companyService = new CompanyService(new UnitOfWork(new EmsDevEntities()), new EmailService());
            var response = companyService.CreateCompanyWithUser(company, "test");
            Assert.AreEqual(true, response.IsSuccess);
        }

        [TestMethod]
        public void CreateCompanyWithUser_ExistingUserName()
        {
            var companyService = new CompanyService(new UnitOfWork(new EmsDevEntities()), new EmailService());
            company.Users.FirstOrDefault().UserAccount.UserName = "franciscebu";
            var response = companyService.CreateCompanyWithUser(company, "test");
            Assert.AreEqual(false, response.IsSuccess);
        }

        [TestMethod]
        public void CreateCompanyWithUser_ExistingEmailAddress()
        {
            var companyService = new CompanyService(new UnitOfWork(new EmsDevEntities()), new EmailService());
            company.Users.FirstOrDefault().EmailAddress= "fcebu@gmail.com";
            var response = companyService.CreateCompanyWithUser(company, "test");
            Assert.AreEqual(false, response.IsSuccess);
        }

        [TestMethod]
        public void CreateCompanyWithUser_IncompleteDetails()
        {
            var companyService = new CompanyService(new UnitOfWork(new EmsDevEntities()), new EmailService());
            company.Name = null;
            company.Users.FirstOrDefault().FirstName = null;
            company.Users.FirstOrDefault().LastName= null;
            var response = companyService.CreateCompanyWithUser(company, "test");
            Assert.AreEqual(false, response.IsSuccess);
        }
    }
}
