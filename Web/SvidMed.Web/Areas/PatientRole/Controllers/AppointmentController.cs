using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SvidMed.Services.Data.Appointments;
using SvidMed.Services.Data.Doctors;
using SvidMed.Services.Data.Patients;
using SvidMed.Services.Data.Ratings;
using SvidMed.Services.DateTimeParser;
using SvidMed.Web.ViewModels.Administration.AppointmentViewModels;
using SvidMed.Web.ViewModels.DoctorViewModels;

namespace SvidMed.Web.Areas.PatientRole.Controllers
{
    public class AppointmentController : PatientRoleController
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IDateTimeParserService _dateTimeParserService;
        private readonly IPatientService _patientService;
        private readonly IRatingService _ratingService;
        private readonly IDoctorService _doctorService;

        public AppointmentController(IAppointmentService appointmentService, IDateTimeParserService dateTimeParserService, IPatientService patientService, IRatingService ratingService, IDoctorService doctorService)
        {
            _appointmentService = appointmentService;
            _dateTimeParserService = dateTimeParserService;
            _patientService = patientService;
            _ratingService = ratingService;
            _doctorService = doctorService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var patientId = await _patientService.GetPatientIdByUserId(userId);

            var viewModel = new AppointmentsListDoctorViewModel
            {
                PastAppointments = await _appointmentService.GetPastByPatientAsync<AppointmentViewDoctorModel>(patientId.Value),
                Appointments = await _appointmentService.GetUpcomingByPatientAsync<AppointmentViewDoctorModel>(patientId.Value),
            };
            return View(viewModel);
        }

        [Authorize]
        public IActionResult MakeAnAppointment(int doctorId)
        {
            var viewModel = new AppointmentMakeFormModel
            {
                DoctorId = doctorId
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> MakeAnAppointment(AppointmentMakeFormModel input)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(MakeAnAppointment), new { input.DoctorId });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var patientId = await _patientService.GetPatientIdByUserId(userId); // add error if user is not a patient
            if (patientId == null)
            {
                return new StatusCodeResult(404);
            }

            DateTime dateTime;
            try
            {
                dateTime = _dateTimeParserService.ConvertStrings(input.Date, input.Time.Replace(" ", ""));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(MakeAnAppointment), new { input.DoctorId });
            }

            return await _appointmentService.AddAsync(input.DoctorId, patientId.Value, dateTime) == false ? RedirectToAction("MakeAnAppointment", new { input.DoctorId }) : RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            await _appointmentService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            var viewModel = await _appointmentService.GetByIdAsync<AppointmentViewDoctorModel>(id);

            if (viewModel == null)
            {
                return new StatusCodeResult(404);
            }

            return View(viewModel);
        }

        [Authorize]
        public IActionResult RateAppointment(int appointmentId)
        {
            var viewModel = new AppointmentRateFormModel
            {
                Doctor = _doctorService.GetDoctorByAppointmentId<DoctorSimplifiedViewModel>(appointmentId),
                AppointmentId = appointmentId
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RateAppointment(AppointmentRateFormModel input)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(RateAppointment), new { input.AppointmentId });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var appointment = await _appointmentService.GetByUserIdAsync(userId, input.AppointmentId);

            if (appointment == null)
            {
                return new StatusCodeResult(404);
            }

            if (appointment.IsRated)
            {
                return new StatusCodeResult(404);
            }

            var patientId = await _patientService.GetPatientIdByUserId(userId);
            if (patientId == null)
            {
                return new StatusCodeResult(404);
            }

            var doctorId = _appointmentService.GetDoctorIdByAppointmentId(input.AppointmentId);

            if (doctorId == null)
            {
                return new StatusCodeResult(404);
            }

            await _ratingService.AddAsync(input.AppointmentId, doctorId.Value, patientId.Value, input.Number, input.Comment);

            return RedirectToAction(nameof(Index));
        }
    }
}
