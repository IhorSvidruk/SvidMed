using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SvidMed.Services.Data.Home;
using SvidMed.Services.Data.Specializations;
using SvidMed.Services.Data.Towns;
using SvidMed.Web.ViewModels;
using SvidMed.Web.ViewModels.HomeViewModels;
using SvidMed.Web.ViewModels.Index;

namespace SvidMed.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        private readonly ITownService _townService;
        private readonly ISpecializationService _specializationService;
        private readonly IHomeService _homeService;

        public HomeController(ITownService townService, ISpecializationService specializationService, IHomeService homeService)
        {
            _townService = townService;
            _specializationService = specializationService;
            _homeService = homeService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel()
            {
                TownItems = _townService.GetAllAsKeyValuePairs(),
                SpecializationItems = _specializationService.GetAllAsKeyValuePairs()
            };
            return View(viewModel);
        }

        [Route("/Home/Error/404")]
        public IActionResult Error404()
        {
            return View();
        }

        [Route("/Home/Error/400")]
        public IActionResult Error400()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(FeedbackCreateFormModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            await _homeService.CreateAsync(input);

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
