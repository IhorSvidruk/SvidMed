﻿using System.ComponentModel.DataAnnotations;

namespace SvidMed.Web.ViewModels.HomeViewModels
{
    public class FeedbackCreateFormModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Select a type.")]
        public string Type { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 20)]
        public string Comment { get; set; }
    }
}

