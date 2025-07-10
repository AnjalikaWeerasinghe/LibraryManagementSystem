namespace Library.Models
{
    public class DivisionStaff
    {
        public int Id { get; set; }
        public int DivisionId { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public LibraryDivision Division { get; set; }
    }
}