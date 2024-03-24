using SvidMed.Data.Models;
using SvidMed.Services.Mapping;

namespace SvidMed.Web.ViewModels.Administration.SpecializationViewModels
{
    public class SpecializationViewModel : IMapFrom<Specialization>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
