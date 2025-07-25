using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            var vm = new GenreViewModel
            {
                PagedGenres = _genre.GetAll(pageNumber, pageSize),
                
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult CreateorUpdate(GenreViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.PagedGenres = _genre.GetAll(1, 10);
            
            }

            if (vm.Id == 0)
            {
                var result =_genre.InsertGenre(vm);
                if (result == "Genre already exists.")
                {
                    TempData["ErrorMessage"] = result;
                }
                else
                {
                    TempData["SuccessMessage"] = $"{vm.Name} successfully Added!";
                }
            }
            else
            {
                _genre.UpdateGenre(vm);
                TempData["SuccessMessage"] = $"{vm.Name} updated successfully !";
                ModelState.Clear();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var genre = _genre.GetGenreById(id);
            if (genre == null)
                return NotFound();

            var pagedData = _genre.GetAll(1, 10);

            var vm = new GenreViewModel
            {
                Id = genre.Id,
                Name = genre.Name,
                Description = genre.Description,
                PagedGenres = pagedData
            };

            return View("Index", vm);
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
