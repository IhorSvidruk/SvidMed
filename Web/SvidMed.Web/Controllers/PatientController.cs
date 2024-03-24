using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SvidMed.Common;
using SvidMed.Data.Models;
using SvidMed.Services.Data.Patients;
using SvidMed.Services.Data.Towns;
using SvidMed.Web.ViewModels.PatientViewModels;

namespace SvidMed.Web.Controllers
{
    [Authorize]
    public class PatientController : BaseController
    {
        private readonly IPatientService _patientService;
        private readonly ITownService _townService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public PatientController(IPatientService patientService, ITownService townService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _patientService = patientService;
            _townService = townService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Create()
        {
            var viewModel = new PatientCreateFormModel
            {
                TownItems = _townService.GetAllAsKeyValuePairs()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PatientCreateFormModel input)
        {
            if (!ModelState.IsValid)
            {
                input.TownItems = _townService.GetAllAsKeyValuePairs();
                return View(input);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.FindByIdAsync(userId);

            input.UserId = userId;

            await _patientService.CreateAsync(input, input.UserId);

            await _userManager.AddToRoleAsync(user, GlobalConstants.PatientRoleName);

            await _signInManager.RefreshSignInAsync(user);

            // TODO Redirect to your profile
            return Redirect("/");
        }
    }
}
