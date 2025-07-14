using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class MemberViewModel : ApplicationUser
    {
        public int Id { get; set; }
        public DateTime MembershipDate { get; set; }
        public int? MembershipTypeId { get; set; }
        public string ApplicatioUserId { get; set; } //Foreign key to Application User

        public MemberViewModel()
        {
        }

        public MemberViewModel(Member model)
        {
            Id = model.Id;
            MembershipDate = model.MembershipDate;
            MembershipTypeId = model.MembershipTypeId;
            ApplicatioUserId = model.ApplicatioUserId;
        }

        public Member ConvertViewModel(MemberViewModel model)
        {
            return new Member
            {
                Id = model.Id,
                MembershipDate = model.MembershipDate,
                MembershipTypeId = model.MembershipTypeId,
                ApplicatioUserId = model.ApplicatioUserId
            };
        }


    }
}
