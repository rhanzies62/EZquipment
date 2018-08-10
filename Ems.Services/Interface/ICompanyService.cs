using Ems.Domain.Common;
using Ems.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Services.Interface
{
    public interface ICompanyService
    {
        Response<bool> CreateCompanyWithUser(CompanyDto model, string createdBy);
        Response<bool> ValidateEmail(string validationToken);
        Response<bool> ResendEmailActivation(string emailAddress);
    }
}
