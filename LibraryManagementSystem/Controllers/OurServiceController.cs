using Library.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    
    public class OurServiceController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUnitOfWork _unitOfWork;

        public OurServiceController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
