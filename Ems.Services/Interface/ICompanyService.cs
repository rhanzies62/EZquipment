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
        Response CreateCompanyWithUser(CompanyDto model, string createdBy);
        Response ValidateEmail(string validationToken);
        Response ResendEmailActivation(string emailAddress);
    }
}
