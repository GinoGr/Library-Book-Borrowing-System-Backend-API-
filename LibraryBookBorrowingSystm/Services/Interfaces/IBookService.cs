using LibraryBookBorrowingSystm.DTOs.Requests;
using LibraryBookBorrowingSystm.DTOs.Responses;

namespace LibraryBookBorrowingSystm.Services.Interfaces;

public interface IBookService
{
    Task<IEnumerable<BookResponse>> GetAllAsync();
    Task<BookResponse> GetByIdAsync(Guid id);
    Task<BookResponse> AddAsync(CreateBookRequest request);
    Task<BookResponse> UpdateAsync(Guid id, UpdateBookRequest request);
    Task DeleteAsync(Guid id);
}
