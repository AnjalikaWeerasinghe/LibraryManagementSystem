using Library.Models;
using Library.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class PublisherViewModel : IValidatableObject
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^(?:\+94|0)?7[0-9]{8}$", ErrorMessage = "Invalid Phone Number.")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Landline Number")]
        [RegularExpression(@"^(?:\+94|0)[1-9][0-9]{8}$", ErrorMessage = "Invalid Landline Number.")]
        public string? Landline { get; set; }

        public PagedResult<PublisherViewModel> PagedPublishers { get; set; }

        public class PublisherSearchViewModel
        {
            public string SearchTerm { get; set; }
            public PagedResult<PublisherViewModel> Result { get; set; }
        }

        public PublisherViewModel()
        {
        }

        public PublisherViewModel(Publisher model)
        {
            Id = model.Id;
            Name = model.Name;
            Address = model.Address;
            PhoneNumber = model.PhoneNumber;
            Landline = model.Landline;
        }

        public Publisher ConvertViewModel(PublisherViewModel model)
        {
            return new Publisher
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                Landline = model.Landline
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(PhoneNumber) && string.IsNullOrWhiteSpace(Landline))
            {
                yield return new ValidationResult(
                    "At least one of Phone Number or Landline Number must be provided.",
                    new[] { nameof(PhoneNumber), nameof(Landline) });
            }
        }
    }
}
