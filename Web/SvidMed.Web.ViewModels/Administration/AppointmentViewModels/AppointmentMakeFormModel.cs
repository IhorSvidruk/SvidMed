using System.ComponentModel.DataAnnotations;
using SvidMed.Common.Attributes;

namespace SvidMed.Web.ViewModels.Administration.AppointmentViewModels
{
    public class AppointmentMakeFormModel
    {
        [Required]
        [ValidateDateString]
        public string Date { get; set; }

        [ValidateTimeString]
        public string Time { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public int PatientId { get; set; }
    }
}
