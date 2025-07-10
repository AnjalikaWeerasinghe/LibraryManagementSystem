using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class StaffViewModel
    {
        public int Id { get; set; }
        public int DivisionId { get; set; }
        public string ApplicationUserId { get; set; } //Foreign key to Application User

        public StaffViewModel()
        {
            
        }

        public StaffViewModel(DivisionStaff model)
        {
            Id = model.Id;
            DivisionId = model.DivisionId;
            ApplicationUserId = model.ApplicationUserId;
        }

        public DivisionStaff ConvertViewModelToModel(StaffViewModel model)
        {
            return new DivisionStaff
            {
                Id = model.Id,
                DivisionId = model.DivisionId,
                ApplicationUserId = model.ApplicationUserId,
            };
        }
    }
}
