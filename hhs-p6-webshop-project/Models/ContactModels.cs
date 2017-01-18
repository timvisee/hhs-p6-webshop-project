﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace hhs_p6_webshop_project.Models
{
        public class ContactModels
        {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Comment is required")]
        public string Comment { get; set; }
        [Required(ErrorMessage = "Reference is required")]
        public string Reference { get; set; }
    }
    }