using System.ComponentModel.DataAnnotations;

namespace LibraryBookBorrowingSystm.DTOs.Requests;

public class CreateMemberRequest
{
    [Required]
    public required string FullName { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }
}
