using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Acquisition
    {
        public int Id { get; set; }
        public int AcquisitionTypeId { get; set; }
        public string AcquisitionCode { get; set; }
        public DateTime AcquisitionDate { get; set; }
        public int? VendorId { get; set; }
        public int? DonorId { get; set; }
        public string Description { get; set; }

        public AcquisitionType AcquisitionType { get; set; }
        public Vendor Vendor { get; set; }
        public Donor Donor { get; set; }

        public ICollection<AcquisitionItem> AcquisitionItems { get; set; }
    }
}
