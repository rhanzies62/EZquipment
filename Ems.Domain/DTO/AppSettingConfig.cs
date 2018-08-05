using System;
using System.Collections.Generic;
using System.Text;

namespace Ems.Domain.DTO
{
    public class AppSettingConfig
    {
        public string Url { get; set; }
        public string EncryptionKey { get; set; }
        public int MaxLoginErrorCount { get; set; }
    }

    public class SmtpConfig
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
