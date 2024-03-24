using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SvidMed.Data.Common.Repositories;
using SvidMed.Data.Models;
using SvidMed.Services.Mapping;
using SvidMed.Web.ViewModels.Administration.SpecializationViewModels;

namespace SvidMed.Services.Data.Specializations
{
    public class SpecializationService : ISpecializationService
    {
        private readonly IDeletableEntityRepository<Specialization> _specializationsRepository;

        public SpecializationService(IDeletableEntityRepository<Specialization> specializationsRepository)
        {
            _specializationsRepository = specializationsRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return _specializationsRepository.AllAsNoTracking() // better to use AsNoTracking to save some memory
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                })
               .ToList()
               .Select(s => new KeyValuePair<string, string>(s.Id.ToString(), s.Name));
        }

        public async Task CreateAsync(SpecializationCreateFormModel model)
        {
            var specialization = new Specialization
            {
                Name = model.Name,
                Description = model.Description
            };

            await _specializationsRepository.AddAsync(specialization);
            await _specializationsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>() => await _specializationsRepository.All().To<T>().ToListAsync();

        public Task<T> GetSpecializationByIdAsync<T>(int specializationId) => _specializationsRepository.All().Where(s => s.Id == specializationId).To<T>().FirstOrDefaultAsync(); // has to track

        public async Task<bool> EditAsync(int specializationId, string name, string description)
        {
            var specialization = await GetSpecializationByIdAsync(specializationId);

            if (specialization == null)
            {
                return false;
            }

            specialization.Name = name;
            specialization.Description = description;

            _specializationsRepository.Update(specialization);

            await _specializationsRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int specializationId) // potential spaghetti code
        {
            var specialization = await GetSpecializationByIdAsync(specializationId);

            if (specialization == null)
            {
                return false;
            }

            _specializationsRepository.Delete(specialization);

            await _specializationsRepository.SaveChangesAsync();

            return true;
        }

        private Task<Specialization> GetSpecializationByIdAsync(int specializationId) => _specializationsRepository.All().FirstOrDefaultAsync(s => s.Id == specializationId);
    }
}
