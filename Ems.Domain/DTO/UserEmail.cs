using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ems.Domain.DTO
{
    public class UserEmailDto
    {
        public int UserId { get; set; }

        [Required, MaxLength(30)]
        public string EmailAddress { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public bool IsPrimary { get; set; }
    }
}
