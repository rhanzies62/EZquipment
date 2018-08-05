using System;
using System.Collections.Generic;
using System.Text;

namespace Ems.Domain.DTO
{
    public class AuthTokenDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }

    public class LoginDetailDto
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public string Name { get; set; }
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
    }
}
