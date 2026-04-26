using System.ComponentModel.DataAnnotations;

namespace LibraryBookBorrowingSystm.DTOs.Requests;

public class CreateBookRequest
{
    [Required]
    public required string Title { get; set; }

    [Required]
    public required string Author { get; set; }

    [Required]
    public required string ISBN { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "TotalCopies must be greater than 0.")]
    public int TotalCopies { get; set; }
}
