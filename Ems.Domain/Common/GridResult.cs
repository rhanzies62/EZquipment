using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Domain.Common
{
    public class GridResult
    {
        public dynamic DataObject { get; set; }
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
    }
}
