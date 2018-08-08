using Ems.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.ExternalServices.Interface
{
    public interface IEmailService
    {
        Response SendEmailActivation(string fullName, string validationLink, string email);
    }
}
