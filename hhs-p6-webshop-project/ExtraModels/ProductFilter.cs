using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hhs_p6_webshop_project.Models.ProductModels;

namespace hhs_p6_webshop_project.ExtraModels
{
    public class ProductFilter
    {
        public ProductFilter() {
            Values = new HashSet<PropertyValue>();
        }

        public PropertyType FilterType { get; set; }
        public HashSet<PropertyValue> Values { get; set; }

        public override string ToString() {
            StringBuilder b = new StringBuilder();
            b.Append($"{FilterType.Name} -> ");

            foreach (var val in Values)
                b.Append($"{val.Value}, ");

            return b.ToString().Substring(0, b.Length - 2);

        }
    }
}
