using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Domain.DTO
{
    public class SecurityProfileDto
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int AccessMenuId { get; set; }
        public bool HasAccess { get; set; }
        public AccessMenuDto AccessMenu { get; set; }
    }
}
