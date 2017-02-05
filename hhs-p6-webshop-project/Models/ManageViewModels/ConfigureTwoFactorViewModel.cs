using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace hhs_p6_webshop_project.Models.ManageViewModels
{
    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }

        public ICollection<SelectListItem> Providers { get; set; }
    }
}