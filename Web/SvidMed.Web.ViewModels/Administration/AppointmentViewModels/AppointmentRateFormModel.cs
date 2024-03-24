using System.ComponentModel.DataAnnotations;
using SvidMed.Web.ViewModels.DoctorViewModels;

namespace SvidMed.Web.ViewModels.Administration.AppointmentViewModels
{
    public class AppointmentRateFormModel
    {
        [Required]
        [Range(1, 5)]
        [Display(Name = "Rating")]
        public int Number { get; set; }

        [MaxLength(250)]
        public string Comment { get; set; }

        public int AppointmentId { get; set; }

        public DoctorSimplifiedViewModel Doctor { get; set; }

        public int DoctorId { get; set; }

        public int PatientId { get; set; }
    }
}
