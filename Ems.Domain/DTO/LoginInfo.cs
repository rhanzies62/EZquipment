using System;
using System.Collections.Generic;
using System.Text;

namespace Ems.Domain.DTO
{
    public class LoginInfoDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
    }
}
