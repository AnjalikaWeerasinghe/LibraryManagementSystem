using Library.Models;
using Library.Repositories.Interfaces;
using Library.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ItemCategoryCountViewModel>> GetCountsAsync()
        {
            var flat = await _unitOfWork.GenericRepository<LibraryItem>()
                .GetAll()  
                .Select(i => new
                {
                    ItemType = i.GetType().Name,
                    CategoryName = i.Category.Name // rename to avoid conflict with 'Name'
                })
                .ToListAsync();  // SQL ends here

            var data = flat
                .GroupBy(x => new { x.ItemType, x.CategoryName })
                .Select(g => new ItemCategoryCountViewModel
                {
                    ItemType = g.Key.ItemType.ToString(),
                    Category = g.Key.CategoryName,
                    Count = g.Count()
                })
                .OrderBy(x => x.ItemType)
                .ThenBy(x => x.Category)
                .ToList();

            return data;
        }

        public async Task<MemberRegistrationChartViewModel> GetMemberRegistrationReportAsync()
        {
            var events = _unitOfWork.GenericRepository<EventParticipant>()
                .GetAll(includeProperties: "LibraryEvent")
                .ToList(); 

            var registrations = events
                .GroupBy(r => r.LibraryEvent.Title)
                .Select(g => new EventRegistrationReportViewModel
                {
                    EventTitle = g.Key,
                    RegistrationCount = g.Count()
                })
                .ToList();

            var chartData = registrations.Select(r => new
            {
                label = r.EventTitle,
                value = r.RegistrationCount
            });

            var chartJson = System.Text.Json.JsonSerializer.Serialize(
                chartData.ToList(),
                new System.Text.Json.JsonSerializerOptions
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

            return new MemberRegistrationChartViewModel
            {
                Registrations = registrations,
                ChartJson = chartJson
            };
        }
    } 
}

