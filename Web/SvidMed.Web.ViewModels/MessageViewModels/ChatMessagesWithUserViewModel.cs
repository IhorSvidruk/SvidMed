﻿using AutoMapper;
using SvidMed.Common;
using SvidMed.Data.Models;
using SvidMed.Services.Mapping;

namespace SvidMed.Web.ViewModels.MessageViewModels
{
    public class ChatMessagesWithUserViewModel : IMapFrom<Message>, IHaveCustomMappings
    {
        public string Content { get; set; }

        public string SenderId { get; set; }

        public string FullName { get; set; }

        public string CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Message, ChatMessagesWithUserViewModel>()
                .ForMember(vm => vm.CreatedOn, opt =>
                    opt.MapFrom(m => m.CreatedOn.AddHours(3).ToString(GlobalConstants.DateTimeFormats.DateTimeFormat)))
                .ForMember(vm => vm.FullName, opt =>
                    opt.MapFrom(m => m.Sender.Doctor.FirstName != null
                        ? "Dr. " + m.Sender.Doctor.FirstName + " " + m.Sender.Doctor.LastName
                        : m.Sender.Patient.FirstName + " " + m.Sender.Patient.LastName));
        }
    }
}
