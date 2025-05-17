namespace Library.Models
{
    public class AcquisitionType
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public ICollection<Acquisition> Acquisitions { get; set; }
    }
}