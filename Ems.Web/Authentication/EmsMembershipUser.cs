using Ems.Domain.Common;
using Ems.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Ems.Web.Authentication
{
    public class EmsMembershipUser : MembershipUser
    {
        #region User Properties  

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<RoleDto> Roles { get; set; }
        public Response<LoginInfoDto> Response { get; set; }
        #endregion

        public EmsMembershipUser(Response<LoginInfoDto> response) : base("EmsMembership", 
                                                            response.Data.UserName,
                                                            response.Data.UserId,
                                                            response.Data.EmailAddress,
                                                            string.Empty, 
                                                            string.Empty, 
                                                            true, 
                                                            false,
                                                            DateTime.Now,
                                                            DateTime.Now, 
                                                            DateTime.Now, 
                                                            DateTime.Now, 
                                                            DateTime.Now)
        {
            UserId = response.Data.UserId;
            FirstName = response.Data.FirstName;
            LastName = response.Data.LastName;
            Roles = response.Data.Roles;
            Response = response;
        }
    }
}