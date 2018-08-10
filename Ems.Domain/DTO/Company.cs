using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ems.Domain.DTO
{
    public class CompanyDto
    {
        public string CompanyCode { get; set; }

        public int? PhoneId { get; set; }

        public int Id { get; set; }

        public int? AddressId { get; set; }

        string _name;
        public string Name {
            get {
                return _name?.ToUpper().Trim();
            }
            set { _name = value; }
        }

        public string Background { get; set; }

        public int? Founded { get; set; }

        public AddressDto Address { get; set; }

        public PhoneDto Phone { get; set; }

        public IEnumerable<UserDto> Users { get; set; }
    }
}
