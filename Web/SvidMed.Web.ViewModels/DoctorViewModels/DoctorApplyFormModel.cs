﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using SvidMed.Common;
using SvidMed.Common.Attributes;

namespace SvidMed.Web.ViewModels.DoctorViewModels
{
    public class DoctorApplyFormModel
    {
        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        public string LastName { get; set; }

        [DataType(DataType.Upload)]
        [Required]
        [ValidateImageFile(ErrorMessage = GlobalConstants.ErrorMessages.Image)]
        public IFormFile Image { get; set; }

        [Range(18, 99)]
        public int Age { get; set; }

        [MinLength(6)]
        public string PhoneNumber { get; set; } // potentially stationary/telephone

        [Display(Name = "Experience (in years)")]
        public int? Experience { get; set; } // years

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        public string Address { get; set; }

        public string Biography { get; set; }

        [Required(ErrorMessage = "Select a town.")]
        [Display(Name = "Town")]
        public int TownId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> TownItems { get; set; }

        [Required(ErrorMessage = "Select a specialization.")]
        [Display(Name = "Specialization")]
        public int SpecializationId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> SpecializationItems { get; set; }

        public string UserId { get; set; } // not sure if needed
    }
}
