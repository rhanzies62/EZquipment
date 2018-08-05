using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Domain.Common
{
    public class GridFilters
    {
        public List<GridFilter> Filters { get; set; }
        public string Logic { get; set; }
    }

    public class GridFilter
    {
        public string Operator { get; set; }
        public string Field { get; set; }
        public string _value { get; set; }
    }
}
