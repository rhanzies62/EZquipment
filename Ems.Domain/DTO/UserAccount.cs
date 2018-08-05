using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ems.Domain.DTO
{
    public class UserAccountDto
    {
        [Required, MaxLength(20)]
        public string UserName { get; set; }

        [Required, MaxLength(100)]
        public string Password { get; set; }

        public string CompanyCode { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public bool IsLock { get; set; }

        public string IPAddress { get; set; }
    }
}
