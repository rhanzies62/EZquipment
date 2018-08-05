using Ems.Domain.DTO;
using Ems.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ems.Web.ExtensionHelper
{
    public static class MapperExtension
    {
        public static CompanyDto MapToCompanyDto(this CompanyRegistrationViewModel model)
        {
            var company = new CompanyDto()
            {
                Name = model.CompanyName,
                Founded = model.Yearfounded,
                Background = model.Background,
                Users = new List<UserDto> {
                        new UserDto
                        {
                             FirstName = model.FirstName,
                             LastName = model.LastName,
                             EmailAddress = model.Email,
                             UserAccount = new UserAccountDto
                             {
                                 UserName = model.Username,
                                 Password = model.Password
                             }
                        }
                    }
            };
            return company;
        }
    }
}