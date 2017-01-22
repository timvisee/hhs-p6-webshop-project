using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Models.FilterModels
{
    public class PriceFilter : FilterBase
    {
        public override string Name {
            get { return "Prijs"; }
        }

        public PriceFilter() {
            
        }

        public PriceFilter(double min, double max) {
            Min = min;
            Max = max;
        }

        public double Min { get; set; }
        public double Max { get; set; }

         public override string ToString() {
            return $"({Name}, (Min -> {Min}, Max -> {Max}))";
        }
    }
}
