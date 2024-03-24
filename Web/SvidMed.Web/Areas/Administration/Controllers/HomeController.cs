using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SvidMed.Services.Data.Home;
using SvidMed.Web.ViewModels.HomeViewModels;

namespace SvidMed.Web.Areas.Administration.Controllers
{
    public class HomeController : AdministrationController
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Feedbacks()
        {
            var viewmodel = await _homeService.GetAllFeedbacksAsync<FeedbackViewModel>();

            return View(viewmodel);
        }

        public async Task<IActionResult> Solve(int feedbackId)
        {
            await _homeService.SolveAsync(feedbackId);

            return RedirectToAction(nameof(Feedbacks));
        }
    }
}
