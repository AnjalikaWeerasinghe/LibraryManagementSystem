using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class EventParticipationAdminViewModel
    {
        public string EventTitle { get; set; }
        public DateTime StartDate { get; set; }
        public string Location { get; set; }

        public List<ParticipantDetail> Participants { get; set; }

        public class ParticipantDetail
        {
            public string FullName { get; set; }
            public string UserCode { get; set; }
            public ParticipantStatus Status { get; set; }
        }
    }
}
