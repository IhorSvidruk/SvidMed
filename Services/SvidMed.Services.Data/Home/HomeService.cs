﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SvidMed.Data.Common.Repositories;
using SvidMed.Data.Models;
using SvidMed.Services.Mapping;
using SvidMed.Web.ViewModels.HomeViewModels;

namespace SvidMed.Services.Data.Home
{
    public class HomeService : IHomeService
    {
        private readonly IDeletableEntityRepository<Feedback> _feedbackRepository;

        public HomeService(IDeletableEntityRepository<Feedback> feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task CreateAsync(FeedbackCreateFormModel model)
        {
            var feedback = new Feedback()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Type = model.Type,
                Comment = model.Comment,
                IsSolved = false
            };

            await _feedbackRepository.AddAsync(feedback);
            await _feedbackRepository.SaveChangesAsync();
        }

        public async Task SolveAsync(int feedbackId)
        {
            var feedback = _feedbackRepository.All().FirstOrDefault(d => d.Id == feedbackId);

            feedback.IsSolved = true;

            await _feedbackRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllFeedbacksAsync<T>() => await _feedbackRepository.All().Where(f => !f.IsSolved.HasValue).To<T>().ToListAsync();
    }
}
