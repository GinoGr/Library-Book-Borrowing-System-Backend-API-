namespace LibraryBookBorrowingSystm.DTOs.Responses;

public class BorrowRecordResponse
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid MemberId { get; set; }
    public required string BookTitle { get; set; }
    public required string MemberName { get; set; }
    public DateTimeOffset BorrowDate { get; set; }
    public DateTimeOffset? ReturnDate { get; set; }
    public required string Status { get; set; }
}
