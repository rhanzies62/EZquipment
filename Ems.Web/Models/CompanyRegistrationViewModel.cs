using Ems.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ems.Web.Models
{
    public class CompanyRegistrationViewModel
    {
        public string CompanyName { get; set; }
        public int? Yearfounded { get; set; }
        public string Background { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}