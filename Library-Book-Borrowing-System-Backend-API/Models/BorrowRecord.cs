namespace LibraryBookBorrowingSystm.Models;

public class BorrowRecord
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid MemberId { get; set; }
    public DateTime BorrowDate { get; set; }
    
    public DateTime? ReturnDate { get; set; } 
    public string Status { get; set; } = string.Empty; 

    public Book? Book { get; set; }
    public Member? Member { get; set; }
}