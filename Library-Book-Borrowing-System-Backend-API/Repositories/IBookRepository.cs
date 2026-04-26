using LibraryBookBorrowingSystm.Models;

namespace LibraryBookBorrowingSystm.Repositories;

public interface IBookRepository
{
    Task<Book?> GetByIdAsync(Guid id);
    Task UpdateAsync(Book book);
}