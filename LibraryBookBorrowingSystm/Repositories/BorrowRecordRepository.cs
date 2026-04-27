using LibraryBookBorrowingSystm.Data;
using LibraryBookBorrowingSystm.Models;
using LibraryBookBorrowingSystm.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryBookBorrowingSystm.Repositories;

public class BorrowRecordRepository : IBorrowRecordRepository
{
    private readonly ApplicationDbContext _context;

    public BorrowRecordRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BorrowRecord>> GetAllAsync()
    {
        return await _context.BorrowRecords
            .Include(r => r.Book)
            .Include(r => r.Member)
            .ToListAsync();
    }

    public async Task<BorrowRecord?> GetByIdAsync(Guid id)
    {
        return await _context.BorrowRecords
            .Include(r => r.Book)
            .Include(r => r.Member)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<BorrowRecord>> GetByMemberIdAsync(Guid memberId)
    {
        var records = await _context.BorrowRecords
            .Include(r => r.Book)
            .Include(r => r.Member)
            .Where(r => r.MemberId == memberId)
            .ToListAsync();

        return records.OrderByDescending(r => r.BorrowDate);
    }

    public async Task AddAsync(BorrowRecord record)
    {
        await _context.BorrowRecords.AddAsync(record);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(BorrowRecord record)
    {
        _context.BorrowRecords.Update(record);
        await _context.SaveChangesAsync();
    }

    public async Task<BorrowRecord?> GetActiveRecordAsync(Guid bookId, Guid memberId)
    {
        return await _context.BorrowRecords
            .Include(r => r.Book)
            .Include(r => r.Member)
            .FirstOrDefaultAsync(r =>
                r.BookId == bookId &&
                r.MemberId == memberId &&
                r.Status == "Borrowed");
    }
}
