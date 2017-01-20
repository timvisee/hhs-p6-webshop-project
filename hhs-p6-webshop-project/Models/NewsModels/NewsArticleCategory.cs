using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Models.NewsModels {
    public class NewsArticleCategory {
        [ForeignKey("NewsArticle")]
        public int NewsArticleID { get; set; }
        public virtual NewsArticle NewsArticle { get; set; }

        [ForeignKey("NewsCategory")]
        public int NewsCategoryID { get; set; }
        public virtual NewsCategory NewsCategory { get; set; }
    }
}
