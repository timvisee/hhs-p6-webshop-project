using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace hhs_p6_webshop_project.Models.NewsModels
{
    public class NewsCategory
    {
        public NewsCategory()
        {
            NewsArticleCategories = new List<NewsArticleCategory>();
        }

        public int NewsCategoryID { get; set; }
        public string Name { get; set; }

        [Display(Name = "NewsArticleCategories")]
        public virtual ICollection<NewsArticleCategory> NewsArticleCategories { get; set; }
    }

    public class NewsCategoryVM
    {
        public NewsCategory NewsCategory { get; set; }

        [Display(Name = "Courses")]
        public IEnumerable<SelectListItem> NewsArticleList { get; set; }

        [Display(Name = "Courses")]
        public IEnumerable<int> SelectedNewsArticles { get; set; }
    }
}
