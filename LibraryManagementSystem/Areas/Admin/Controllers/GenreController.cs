using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("admin")]
    public class GenreController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IGenreService _genre;

        public GenreController(IGenreService genre, IUnitOfWork unitOfWork)
        {
            _genre = genre;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_genre.GetAll(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _genre.GetGenreById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(GenreViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var genre = _unitOfWork.GenericRepository<Genre>().GetById(vm.Id);
            if (genre == null)
                return NotFound();

            genre.Name = vm.Name;
            genre.Description = vm.Description;

            _unitOfWork.GenericRepository<Genre>().Update(genre);
            _unitOfWork.Save();

            ViewBag.UpdateSuccess = $"Genre, '{genre.Name}' updated successfully!";
            ViewBag.ShowModal = true;

            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(GenreViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                _unitOfWork.GenericRepository<Genre>().Add(new Genre
                {
                    Name = vm.Name,
                    Description = vm.Description
                });
                _unitOfWork.Save();

                TempData["SuccessMessage"] = $"Genre, {vm.Name} successfully Added !";
                ModelState.Clear(); // clear input fields
                return View();
            }
            catch
            {
                ModelState.AddModelError("", "An error occurred while saving.");
                return View(vm);
            }
        }

        public IActionResult Delete(int id)
        {
            var entity = _unitOfWork.GenericRepository<Genre>().GetById(id);
            if (entity == null)
                return NotFound();

            _unitOfWork.GenericRepository<Genre>().Delete(entity);
            _unitOfWork.Save();

            return Ok();
        }
    }
}
