using System.Collections.Generic;
using System.Threading.Tasks;

namespace SvidMed.Services.Data.Ratings
{
    public interface IRatingService
    {
        IEnumerable<T> GetAllRatingsByDoctorId<T>(int doctorId, int count);

        Task AddAsync(int appointmentId, int doctorId, int patientId, int number, string comment);
    }
}
