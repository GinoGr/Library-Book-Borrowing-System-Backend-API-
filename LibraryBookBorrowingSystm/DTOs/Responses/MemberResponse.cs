namespace LibraryBookBorrowingSystm.DTOs.Responses;

public class MemberResponse
{
    public Guid Id { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public DateTimeOffset MembershipDate { get; set; }
}
