using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ems.Domain.DTO
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string StreetAddress { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Long { get; set; }
    }
}
