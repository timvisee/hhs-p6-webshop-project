using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Models.BlogModels {
    public class BlogArticle {
        [Key]
        public int BlogArticleId { get; set; }

        public string Title { get; set; }
        public string ArticleText { get; set; }
        public string ImagePath { get; set; }

        // Multiple categories per article
        public ICollection<BlogArticleCategory> BlogArticleCategories { get; set; }

    }
}
