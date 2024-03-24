using System.Collections.Generic;
using System.Threading.Tasks;
using SvidMed.Web.ViewModels.Administration.TownViewModels;

namespace SvidMed.Services.Data.Towns
{
    public interface ITownService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();

        Task CreateAsync(TownCreateFormModel model);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<T> GetTownByIdAsync<T>(int townId);

        Task<bool> EditAsync(int townId, string name, int? zipCode);

        Task<bool> DeleteAsync(int townId);
    }
}
