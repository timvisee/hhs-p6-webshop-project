using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Api
{
    public class PagedResponse
    {
        public object Data { get; set; }
        public string PreviousPage { get; set; }
        public string NextPage { get; set; }
    }
}
