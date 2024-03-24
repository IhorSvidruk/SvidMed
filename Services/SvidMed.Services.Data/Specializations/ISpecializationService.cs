using System.Collections.Generic;
using System.Threading.Tasks;
using SvidMed.Web.ViewModels.Administration.SpecializationViewModels;

namespace SvidMed.Services.Data.Specializations
{
    public interface ISpecializationService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();

        Task CreateAsync(SpecializationCreateFormModel model);

        Task<T> GetSpecializationByIdAsync<T>(int specializationId);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<bool> EditAsync(int specializationId, string name, string description);

        Task<bool> DeleteAsync(int specializationId);
    }
}
