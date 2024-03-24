using System.ComponentModel.DataAnnotations;
using SvidMed.Data.Models;
using SvidMed.Services.Mapping;

namespace SvidMed.Web.ViewModels.Administration.TownViewModels
{
    public class TownEditFormModel : IMapFrom<Town>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)] // Lom only?
        public string Name { get; set; }

        public int? ZipCode { get; set; }
    }
}
