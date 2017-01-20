using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Models.FilterModel
{
    public class PriceFilter : FilterBase
    {
        public override string Name {
            get { return "Prijs"; }
        }

        public double Min { get; set; }
        public double Max { get; set; }

         public override string ToString() {
            return $"({Name}, (Min -> {Min}, Max -> {Max}))";
        }
    }
}
