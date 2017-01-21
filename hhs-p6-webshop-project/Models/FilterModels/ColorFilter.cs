using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Models.FilterModels
{
    public class ColorFilter : FilterBase
    {
        public ColorFilter() {
            Colors = new List<string>();
        }

        public override string Name {
            get { return "Kleur"; }
        }

        public List<string> Colors { get; set; }

        public override string ToString() {
            return $"({Name}, ({string.Join(", ", Colors)}))";
        }
    }
}
