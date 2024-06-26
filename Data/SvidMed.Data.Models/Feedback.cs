﻿using System.ComponentModel.DataAnnotations;
using SvidMed.Data.Common.Models;

namespace SvidMed.Data.Models
{
    public class Feedback : BaseDeletableModel<int>
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        [MaxLength(500)]
        public string Comment { get; set; }

        public bool? IsSolved { get; set; }
    }
}
