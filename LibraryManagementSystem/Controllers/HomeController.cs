using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;
using Library.Repositories.Interfaces;
using Library.ViewModels;
using Library.Models;

namespace LibraryManagementSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var latestEvents = _unitOfWork.GenericRepository<LibraryEvent>()
            .GetAll()
            .OrderByDescending(e => e.StartDate)
            .Take(5)
            .Where(e => !string.IsNullOrEmpty(e.ImageUrl))
            .Select(e => new LibraryEvent
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                ImageUrl = e.ImageUrl,
                StartDate = e.StartDate
            })
            .ToList();

        ViewBag.LatestEventImages = latestEvents;

        
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
