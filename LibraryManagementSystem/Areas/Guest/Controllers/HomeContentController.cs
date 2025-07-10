using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Areas.Guest.Controllers
{
    public class HomeContentController : Controller
    {
        private IHomeContentService _home;
        private IUnitOfWork _unitOfWork;

        public HomeContentController(IHomeContentService home, IUnitOfWork unitOfWork)
        {
            _home = home;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var content = _unitOfWork.GenericRepository<HomeContent>().GetAll().FirstOrDefault();
            return View(content);
        }

    }
}
