using System.ComponentModel.DataAnnotations;

namespace SvidMed.Web.ViewModels.Administration.SpecializationViewModels
{
    public class SpecializationCreateFormModel
    {
        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }
    }
}
