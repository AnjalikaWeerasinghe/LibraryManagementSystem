namespace Library.Models
{
    public class MembershipType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxItemsAllowed { get; set; }

        public ICollection<Member> Members { get; set; }
    }
}