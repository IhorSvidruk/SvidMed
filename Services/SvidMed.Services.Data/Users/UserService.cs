using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SvidMed.Data.Common.Repositories;
using SvidMed.Data.Models;
using SvidMed.Services.Mapping;

namespace SvidMed.Services.Data.Users
{
    public class UserService : IUserService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> _userRepository;

        public UserService(IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>() => await _userRepository.All().AsNoTracking().To<T>().ToListAsync();

        public async Task<T> GetByIdAsync<T>(string id) => await _userRepository.All().Where(u => u.Id == id).To<T>().FirstOrDefaultAsync();
    }
}
