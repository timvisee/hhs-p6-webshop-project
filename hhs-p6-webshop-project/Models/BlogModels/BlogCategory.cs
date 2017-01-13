using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Models.BlogModels {
    public class BlogCategory {
        [Key]
        public int BlogCategoryId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        // Multiple articles per category
        public ICollection<BlogArticleCategory> BlogArticleCategories { get; set; }
    }
}
