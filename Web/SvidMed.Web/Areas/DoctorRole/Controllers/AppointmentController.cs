using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SvidMed.Services.Data.Appointments;
using SvidMed.Services.Data.Doctors;
using SvidMed.Web.ViewModels.Administration.AppointmentViewModels;

namespace SvidMed.Web.Areas.DoctorRole.Controllers
{
    public class AppointmentController : DoctorRoleController
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;

        public AppointmentController(IAppointmentService appointmentService, IDoctorService doctorService)
        {
            _appointmentService = appointmentService;
            _doctorService = doctorService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var doctorId = _doctorService.GetDoctorIdByUserId(userId);

            var viewModel = new AppointmentsListPatientViewModel
            {
                Appointments = await _appointmentService.GetUpcomingByPatientAsync<AppointmentViewPatientModel>(doctorId.Value),
                PastAppointments = await _appointmentService.GetPastByPatientAsync<AppointmentViewPatientModel>(doctorId.Value)
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmAppointment(int appointmentId)
        {
            await _appointmentService.ConfirmAsync(appointmentId);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeclineAppointment(int appointmentId)
        {
            await _appointmentService.DeclineAsync(appointmentId);
            return RedirectToAction(nameof(Index));
        }
    }
}
