using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Services.Config
{
    public class Utility
    {
        public static string GetConfig(string key)
        {
            string value = ConfigurationManager.AppSettings[key];
            return value;
        }
    }
}
