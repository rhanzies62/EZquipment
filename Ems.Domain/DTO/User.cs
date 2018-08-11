using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ems.Domain.DTO
{
    public class UserDto
    {
        public int UserAccountId { get; set; }

        public string EmailAddress { get; set; }

        public int? PhoneId { get; set; }

        public int Id { get; set; }

        public int? AddressId { get; set; }

        public int CompanyId { get; set; }

        string _firstName;
        [Required, MaxLength(50)]
        public string FirstName
        {
            get
            {
                return _firstName?.ToUpper().Trim();
            }
            set { _firstName = value; }
        }

        string middleName;
        [MaxLength(50)]
        public string MiddleName
        {
            get
            {
                return middleName?.ToUpper().Trim();
            }
            set { middleName = value; }
        }

        string _lastName;
        [Required, MaxLength(50)]
        public string LastName
        {
            get
            {
                return _lastName?.ToUpper().Trim();
            }
            set { _lastName = value; }
        }

        public AddressDto Address { get; set; }

        public UserAccountDto UserAccount { get; set; }

        public virtual CompanyDto Company { get; set; }
    }
}
