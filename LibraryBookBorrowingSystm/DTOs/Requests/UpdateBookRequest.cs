using System.ComponentModel.DataAnnotations;

namespace LibraryBookBorrowingSystm.DTOs.Requests;

public class UpdateBookRequest
{
    [Required]
    public required string Title { get; set; }

    [Required]
    public required string Author { get; set; }

    [Required]
    public required string ISBN { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "TotalCopies must be greater than 0.")]
    public int TotalCopies { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "AvailableCopies must be 0 or greater.")]
    public int AvailableCopies { get; set; }
}
