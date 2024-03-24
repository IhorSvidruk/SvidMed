using SvidMed.Data.Models;
using SvidMed.Services.Mapping;

namespace SvidMed.Web.ViewModels.Administration.TownViewModels
{
    public class TownViewModel : IMapFrom<Town>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ZipCode { get; set; }
    }
}
