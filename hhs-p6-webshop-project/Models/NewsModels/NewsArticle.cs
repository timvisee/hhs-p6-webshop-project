using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace hhs_p6_webshop_project.Models.NewsModels
{
    public class NewsArticle
    {
        public NewsArticle()
        {
            NewsArticleCategories = new List<NewsArticleCategory>();
        }

        public int NewsArticleID { get; set; }
        public string Name { get; set; }

        [Display(Name = "NewsArticleCategories")]
        public virtual ICollection<NewsArticleCategory> NewsArticleCategories { get; set; }
    }

    public class NewsArticleVM
    {
        public NewsArticle NewsArticle { get; set; }

        [Display(Name = "Studenten")]
        public IEnumerable<SelectListItem> NewsCategoriesList { get; set; }

        [Display(Name = "Studenten")]
        public IEnumerable<int> SelectedNewsCategories { get; set; }
    }
}
