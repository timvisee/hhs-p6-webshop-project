using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hhs_p6_webshop_project.Models.BlogModels;

namespace hhs_p6_webshop_project.Models.BlogModels {
    public class BlogArticleViewModel {
        public BlogArticleViewModel() {
            BlogCategories = new List<BlogCategory>();
            BlogArticle = new BlogArticle();
            ActiveBlogCategories = new List<BlogCategory>();
        }

        public List<BlogCategory> BlogCategories { get; set; }

        public List<BlogCategory> ActiveBlogCategories { get; set; }

        public BlogArticle BlogArticle { get; set; }
    }
}
