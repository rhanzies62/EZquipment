using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ems.Domain.DTO;
using System.Collections.Generic;
using Ems.Services;
using Ems.Data;
using Ems.Domain;
using Ems.Services.Config;
using Ems.ExternalServices;

namespace Ems.Service.Test
{
    [TestClass]
    public class CompanyServiceTest
    {
        [TestMethod]
        public void CreateCompanyWithUser_CompleteDetails_Success()
        {
            AutoMapperConfig.Init();
            var companyService = new CompanyService(new UnitOfWork(new EmsDevEntities()),new EmailService());
            #region company dto
            CompanyDto company = new CompanyDto
            {
                Name = "Basecamp",
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
                                EmailAddress = "fcebu@gmail.com",
                            }
                }
            };
            #endregion
            companyService.CreateCompanyWithUser(company, "test");
        }
    }
}
