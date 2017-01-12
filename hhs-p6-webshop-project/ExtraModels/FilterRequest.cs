using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.ExtraModels
{
    public class FilterRequest
    {
        public FilterRequest() {
            Values = new Dictionary<int, List<int>>();
        }

        public Dictionary<int, List<int>> Values { get; set; }
    }
}
