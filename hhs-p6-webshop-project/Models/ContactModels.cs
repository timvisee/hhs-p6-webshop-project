using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace hhs_p6_webshop_project.Models
{
    public class ContactModels
    {
        [DisplayName("Naam")]
        [Required(ErrorMessage = "Naam is verplicht")]
        public string Name { get; set; }

        [DisplayName("Telefoonnummer")]
        [Required(ErrorMessage = "Phone is verplicht")]
        public string Phone { get; set; }

        [DisplayName("E-Mail")]
        [Required(ErrorMessage = "Email is verplicht")]
        public string Email { get; set; }

        [DisplayName("Bericht")]
        [Required(ErrorMessage = "Comment is verplicht")]
        public string Comment { get; set; }

        [DisplayName("Referentie")]
        [Required(ErrorMessage = "Reference is verplicht")]
        public string Reference { get; set; }
    }
}