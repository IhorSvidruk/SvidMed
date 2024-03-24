using System.Collections.Generic;
using System.Threading.Tasks;
using SvidMed.Web.ViewModels.HomeViewModels;

namespace SvidMed.Services.Data.Home
{
    public interface IHomeService
    {
        Task CreateAsync(FeedbackCreateFormModel model);

        Task SolveAsync(int feedbackId);

        Task<IEnumerable<T>> GetAllFeedbacksAsync<T>();
    }
}
