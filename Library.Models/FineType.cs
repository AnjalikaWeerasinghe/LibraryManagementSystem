namespace Library.Models
{
    public class FineType
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public ICollection<Fine> Fines { get; set; }
    }
}