using System.ComponentModel.DataAnnotations;

namespace LibraryBookBorrowingSystm.DTOs.Requests;

public class UpdateMemberRequest
{
    [Required]
    public required string FullName { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }
}
