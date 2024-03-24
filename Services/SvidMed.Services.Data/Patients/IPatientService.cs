using System.Threading.Tasks;
using SvidMed.Web.ViewModels.Administration.PatientViewModels;
using SvidMed.Web.ViewModels.PatientViewModels;

namespace SvidMed.Services.Data.Patients
{
    public interface IPatientService
    {
        public Task CreateAsync(PatientCreateFormModel model, string userId);

        Task EditAsync(PatientEditFormModel model);

        Task<int?> GetPatientIdByUserId(string userId);

        T GetPatientById<T>(int patientId);
    }
}
