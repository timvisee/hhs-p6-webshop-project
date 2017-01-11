using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Models.ProductModels {
    public class PropertySet {
        [Key]
        public int PropertySetId { get; set; }

        [Required]
        public string Value { get; set; }

        public int PropertyTypeId { get; set; }
        [ForeignKey("PropertyTypeId")]
        public virtual PropertyType PropertyType { get; set; }
    }
}
