using System.Collections.Generic;
using SvidMed.Web.ViewModels.RatingViewModels;

namespace SvidMed.Web.ViewModels.DoctorViewModels
{
    public class DoctorProfileViewModel
    {
        public IEnumerable<RatingViewModel> Ratings { get; set; }

        public DoctorInListViewModel Doctor { get; set; }
    }
}
