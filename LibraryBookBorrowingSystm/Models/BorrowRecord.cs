namespace LibraryBookBorrowingSystm.Models;

public class BorrowRecord
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid MemberId { get; set; }
    public DateTimeOffset BorrowDate { get; set; }
    public DateTimeOffset? ReturnDate { get; set; }
    public string Status { get; set; } = null!;
    public Book? Book { get; set; }
    public Member? Member { get; set; }
}
