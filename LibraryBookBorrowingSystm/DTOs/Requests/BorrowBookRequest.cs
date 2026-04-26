using System.ComponentModel.DataAnnotations;

namespace LibraryBookBorrowingSystm.DTOs.Requests;

public class BorrowBookRequest
{
    [Required]
    public Guid BookId { get; set; }

    [Required]
    public Guid MemberId { get; set; }
}
