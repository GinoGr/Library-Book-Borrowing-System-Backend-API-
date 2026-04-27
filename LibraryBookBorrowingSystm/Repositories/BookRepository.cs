using LibraryBookBorrowingSystm.Data;
using LibraryBookBorrowingSystm.Models;
using LibraryBookBorrowingSystm.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryBookBorrowingSystm.Repositories;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _context;

    public BookRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        Console.WriteLine("DB HIT");
        return await _context.Books.ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(Guid id)
    {
        return await _context.Books.FindAsync(id);
    }

    public async Task AddAsync(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Books.AnyAsync(b => b.Id == id);
    }

    public async Task<bool> TryDecrementAvailableCopiesAsync(Guid id)
    {
        var rows = await _context.Database.ExecuteSqlRawAsync(
            "UPDATE Books SET AvailableCopies = AvailableCopies - 1 WHERE Id = {0} AND AvailableCopies > 0",
            id);
        return rows > 0;
    }
}
