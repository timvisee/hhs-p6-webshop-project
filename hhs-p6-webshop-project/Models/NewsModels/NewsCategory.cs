﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        [Display(Name = "Naam")]
        public string Name { get; set; }

        [Display(Name = "Categorieen")]
        public virtual ICollection<NewsArticleCategory> NewsArticleCategories { get; set; }
    }

    public class NewsCategoryView
    {
        public NewsCategory NewsCategory { get; set; }

        public List<NewsCategory> NewsCategories { get; set; }
        public List<NewsArticle> NewsArticles { get; set; }

        [Display(Name = "Artikelen")]
        public IEnumerable<SelectListItem> NewsArticleList { get; set; }

        [Display(Name = "Artikelen")]
        public IEnumerable<int> SelectedNewsArticles { get; set; }
    }
}