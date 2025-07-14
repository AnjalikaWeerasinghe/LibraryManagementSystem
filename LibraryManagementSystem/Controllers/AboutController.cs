using Library.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class AboutController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUnitOfWork _unitOfWork;

        public AboutController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult WhoWeAre() => PartialView("_AboutWhoWeAre");
        public IActionResult VisionMission() => PartialView("_AboutVisionMission");
        public IActionResult AimsObjectives() => PartialView("_AboutAimsObjectives");
        public IActionResult Background() => PartialView("_AboutBackground");
        public IActionResult OurServices() => PartialView("_AboutOurServices");

    }
}
