using System.Collections.Generic;
using System.Threading.Tasks;

namespace SvidMed.Services.Data.Users
{
    public interface IUserService
    {
        Task<T> GetByIdAsync<T>(string id);

        Task<IEnumerable<T>> GetAllAsync<T>();
    }
}
