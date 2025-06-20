using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class LibraryInfoViewModel
    {
        public int Id { get; set; }
        public string RegisteredCode { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string OpeningHours { get; set; }
        public string OpeningDays { get; set; }
        public string Description { get; set; }

        public LibraryInfoViewModel()
        {
            
        }

        public LibraryInfoViewModel(LibraryInfo model)
        {
            Id = model.Id;
            RegisteredCode = model.RegisteredCode;
            Name = model.Name;
            Address = model.Address;
            PhoneNumber = model.PhoneNumber;
            Email = model.Email;
            OpeningHours = model.OpeningHours;
            OpeningDays = model.OpeningDays;
            Description = model.Description;
        }

        public LibraryInfo ConvertViewModel(LibraryInfoViewModel model)
        {
            return new LibraryInfo
            {
                Id = model.Id,
                RegisteredCode = model.RegisteredCode,
                Name = model.Name,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                OpeningHours = model.OpeningHours,
                OpeningDays = model.OpeningDays,
                Description = model.Description
            };
        }
    }
}
