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

        [Display(Name = "Titel")]
        public string Name { get; set; }

        [Display(Name = "Afbeelding")]
        public string ImagePath { get; set; }

        [Display(Name = "Samenvatting")]
        public string Excerpt { get; set; }

        [Display(Name = "Bericht")]
        public string Content { get; set; }

        [Display(Name = "Categorieen")]
        public virtual ICollection<NewsArticleCategory> NewsArticleCategories { get; set; }
    }

    public class NewsArticleVM
    {
        public NewsArticle NewsArticle { get; set; }

        [Display(Name = "Nieuwsartikelen")]
        public IEnumerable<SelectListItem> NewsCategoriesList { get; set; }

        [Display(Name = "Nieuwsartikelen")]
        public IEnumerable<int> SelectedNewsCategories { get; set; }
    }
}
