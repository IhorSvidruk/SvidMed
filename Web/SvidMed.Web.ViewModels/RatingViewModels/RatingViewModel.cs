using AutoMapper;
using SvidMed.Common;
using SvidMed.Data.Models;
using SvidMed.Services.Mapping;

namespace SvidMed.Web.ViewModels.RatingViewModels
{
    public class RatingViewModel : IMapFrom<Rating>, IHaveCustomMappings
    {
        public string PatientFullName { get; set; }

        public int Number { get; set; }

        public string Comment { get; set; }

        public string CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Rating, RatingViewModel>()
                .ForMember(vm => vm.PatientFullName, opt =>
                    opt.MapFrom(r => r.Patient.FirstName + " " + r.Patient.LastName))
                .ForMember(vm => vm.CreatedOn, opt =>
                    opt.MapFrom(r => r.CreatedOn.ToString(GlobalConstants.DateTimeFormats.DateTimeFormat)));
        }
    }
}
