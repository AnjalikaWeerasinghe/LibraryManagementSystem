using Library.Models;
using Library.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class BorrowingViewModel
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        [Required]
        public string UserCode { get; set; }

        [Required]
        public int ItemCopyId { get; set; }
        public string ItemCopyCode { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Borrowed Date")]
        public DateTime BorrowedDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Returned Date")]
        public DateTime? ReturnedDate { get; set; }

        public BorrowedStatus BorrowedStatus { get; set; }

        public IEnumerable<SelectListItem> Users { get; set; }
        public IEnumerable<SelectListItem> AvailableItemCopies { get; set; }

        public PagedResult<BorrowingViewModel> PagedBorrowings { get; set; }

        public string ItemTitle { get; set; }
        public string ItemCode { get; set; }

        public BorrowingViewModel()
        {
            
        }

        public BorrowingViewModel(Borrowing model)
        {
            Id = model.Id;
            UserId = model.UserId;
            UserCode = model.ApplicationUser?.UserCode ?? "N/A";
            ItemCopyId = model.ItemCopyId;
            DueDate = model.DueDate;
            ReturnedDate = model.ReturnedDate;
            BorrowedStatus = model.BorrowedStatus;
            BorrowedDate = model.BorrowedDate;

            ItemTitle = model.ItemCopy.LibraryItem.Title;
            ItemCode = model.ItemCopy.LibraryItem.ItemCode;
            ItemCopyCode = model.ItemCopy.ItemCopyCode;
        }

        public Borrowing ToModel(BorrowingViewModel model)
        {
            return new Borrowing
            {
                Id = model.Id,
                UserId = model.UserId,
                ItemCopyId = model.ItemCopyId,
                DueDate = model.DueDate,
                ReturnedDate = model.ReturnedDate,
                BorrowedDate = model.BorrowedDate,
                BorrowedStatus = model.BorrowedStatus,
            };
        }
    }
}
