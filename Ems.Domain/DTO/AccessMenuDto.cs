using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Domain.DTO
{
    public class AccessMenuDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsPanel { get; set; }
        public bool IsSubMenu { get; set; }
        public string InternalName { get; set; }
    }
}
