namespace LibraryBookBorrowingSystm.Models;

public class Member
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTimeOffset MembershipDate { get; set; }
    public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
}
