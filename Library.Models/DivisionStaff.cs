namespace Library.Models
{
    public class DivisionStaff
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int DivisionId { get; set; }
        public UserStatus UserStatus { get; set; }

        public ApplicationUser User { get; set; }
        public LibraryDivision Division { get; set; }
    }
}