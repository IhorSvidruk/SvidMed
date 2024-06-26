﻿using System.Linq;
using AutoMapper;
using SvidMed.Data.Models;
using SvidMed.Services.Mapping;

namespace SvidMed.Web.ViewModels.DoctorViewModels
{
    public class DoctorInListViewModel : IMapFrom<Doctor>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string FullName { get; set; }

        public string ImageUrl { get; set; } // TODO really wanted multiple images, but implementing will take too long

        public int Age { get; set; }

        public string PhoneNumber { get; set; }

        public double? AverageRating { get; set; }

        public int? Experience { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Biography { get; set; }

        public string TownName { get; set; }

        public string SpecializationName { get; set; }

        public bool HasApplied { get; set; }

        public bool IsValidated { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Doctor, DoctorInListViewModel>()
                .ForMember(vm => vm.ImageUrl, opt =>
                    opt.MapFrom(d =>
                        d.Images.FirstOrDefault().RemoteImageUrl != null
                            ? d.Images.FirstOrDefault().RemoteImageUrl
                            : "/img/doctors/" + d.Images.FirstOrDefault().Id + "." +
                              d.Images.FirstOrDefault().Extension))
                .ForMember(vm => vm.FullName, opt =>
                    opt.MapFrom(d => d.FirstName + " " + d.LastName))
                .ForMember(vm => vm.AverageRating, opt =>
                    opt.MapFrom(d => d.Ratings.Average(r => r.Number)));
        }
    }
}
