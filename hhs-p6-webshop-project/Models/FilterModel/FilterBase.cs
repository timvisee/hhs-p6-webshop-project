using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Models.FilterModel
{
    public abstract class FilterBase
    {
        public abstract string Name { get; }

        public override string ToString() {
            return $"({Name})";
        }
    }
}
