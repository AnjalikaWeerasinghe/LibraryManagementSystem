namespace Library.Models
{
    public class LibraryDivision
    {
        public int Id { get; set; }
        public string DivisionName { get; set; }
        public string Description { get; set; }

        public int LibraryInfoId { get; set; }
        public LibraryInfo LibraryInfo { get; set; }

        public ICollection<DivisionStaff> StaffMembers { get; set; }
    }
}