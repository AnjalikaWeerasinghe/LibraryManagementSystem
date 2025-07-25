using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace LibraryManagementSystem.Areas.Member.Controllers
{
    [Area("Member")]
    public class EventController : Controller
    {
        private ILibraryEventService _libraryEvent;
        private IUnitOfWork _unitOfWork;

        public EventController(ILibraryEventService libraryEvent, IUnitOfWork unitOfWork)
        {
            _libraryEvent = libraryEvent;
            _unitOfWork = unitOfWork;
        }
        
        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_libraryEvent.GetAll(pageNumber, pageSize));
        }

        public IActionResult Details(int id)
        {
            var vm = _libraryEvent.GetLibraryEventById(id);
            return View(vm);
        }

        
    }
}
