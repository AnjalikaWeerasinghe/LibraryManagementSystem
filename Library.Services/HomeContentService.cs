using Library.Models;
using Library.Repositories.Interfaces;
using Library.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public class HomeContentService : IHomeContentService
    {
        private IUnitOfWork _unitOfWork;

        public HomeContentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public HomeContentViewModel GetContent()
        {
            var home = _unitOfWork.GenericRepository<HomeContent>().GetAll().FirstOrDefault();

            var events = _unitOfWork.GenericRepository<LibraryEvent>()
                .GetAll()
                .OrderByDescending(e => e.StartDate)
                .Take(5)
                .ToList();

            return new HomeContentViewModel
            {
                Id = home?.Id ?? 0,
                Title = home?.Title ?? "Library Management System",
                Content = home?.Content ?? "",
                CustomLinks = home?.CustomLinks,
                EventImagePaths = events
                    .Where(e => !string.IsNullOrEmpty(e.ImageUrl))
                    .Select(e => e.ImageUrl)
                    .ToList()
            };
        }

        public void UpdateContent(HomeContentViewModel homeContent)
        {
            var model = _unitOfWork.GenericRepository<HomeContent>().GetById(homeContent.Id);
            if (model == null) return;

            model.Title = homeContent.Title;
            model.Content = homeContent.Content;
            model.CustomLinks = homeContent.CustomLinks;

            _unitOfWork.GenericRepository<HomeContent>().Update(model);
            _unitOfWork.Save();
        }
    }
}
