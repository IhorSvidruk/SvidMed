﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using SvidMed.Data.Common.Models;

namespace SvidMed.Data.Models
{
    public class Appointment : BaseDeletableModel<int>
    {
        public DateTime DateTime { get; set; }

        [ForeignKey(nameof(Doctor))]
        public int DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }

        [ForeignKey(nameof(Patient))]
        public int PatientId { get; set; }

        public virtual Patient Patient { get; set; }

        public bool? Confirmed { get; set; }

        public bool IsRated { get; set; }

        [ForeignKey(nameof(Rating))]
        public int? RatingId { get; set; }

        public virtual Rating Rating { get; set; }
    }
}
