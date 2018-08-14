using Ems.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Ems.Web.Authentication
{
    public class AppPrincipal: IPrincipal
    {
        #region Identity Properties  

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }
        public string[] AccessMenus { get; set; }
        #endregion

        public IIdentity Identity
        {
            get; private set;
        }

        public bool IsInRole(string role)
        {
            if (Roles.Any(r => r.Contains(role)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public AppPrincipal(string username)
        {
            Identity = new GenericIdentity(username);
        }
    }
}