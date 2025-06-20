using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class MemberViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string MemberCode { get; set; }
        public DateTime MembershipDate { get; set; }
        public int? MembershipTypeId { get; set; }

        public MemberViewModel()
        {
        }

        public MemberViewModel(Member model)
        {
            Id = model.Id;
            UserId = model.UserId;
            MemberCode = model.MemberCode;
            MembershipDate = model.MembershipDate;
            MembershipTypeId = model.MembershipTypeId;
        }

        public Member ConvertViewModel(MemberViewModel model)
        {
            return new Member
            {
                Id = model.Id,
                UserId = model.UserId,
                MemberCode = model.MemberCode,
                MembershipDate = model.MembershipDate,
                MembershipTypeId = model.MembershipTypeId

            };
        }


    }
}
