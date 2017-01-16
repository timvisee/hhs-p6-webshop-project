using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Models.BlogModels {
    /**
     * This many to many relation is based on the following stackoverflow article:
     * http://stackoverflow.com/questions/29442493/how-to-create-a-many-to-many-relationship-with-latest-nightly-builds-of-ef-core
     */
    public class BlogArticleCategory {
        public int BlogArticleId { get; set; }
        public BlogArticle BlogArticle { get; set; }

        public int BlogCategoryId { get; set; }
        public BlogCategory BlogCategory { get; set; }
    }
}
