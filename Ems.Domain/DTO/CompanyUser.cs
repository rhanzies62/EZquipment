using System;
using System.Collections.Generic;
using System.Text;

namespace Ems.Domain.DTO
{
    public class CompanyUserDto
    {
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public CompanyDto Company { get; set; }
    }
}
