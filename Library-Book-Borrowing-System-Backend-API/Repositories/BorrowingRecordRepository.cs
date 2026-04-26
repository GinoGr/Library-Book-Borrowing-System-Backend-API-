using LibraryBookBorrowingSystm.Data;
using LibraryBookBorrowingSystm.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryBookBorrowingSystm.Repositories;

public class BorrowRecordRepository : IBorrowRecordRepository
{
    private readonly ApplicationDbContext _context;

    public BorrowRecordRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<BorrowRecord> AddAsync(BorrowRecord record)
    {
        _context.BorrowRecords.Add(record);
        await _context.SaveChangesAsync();
        return record;
    }

    public async Task<BorrowRecord?> GetActiveBorrowRecordAsync(Guid memberId, Guid bookId)
    {
        return await _context.BorrowRecords
            .Include(b => b.Book) 
            .FirstOrDefaultAsync(b => b.MemberId == memberId && b.BookId == bookId && b.Status == "Borrowed");
    }

    public async Task<IEnumerable<BorrowRecord>> GetHistoryForMemberAsync(Guid memberId)
    {
        return await _context.BorrowRecords
            .Where(b => b.MemberId == memberId)
            .OrderByDescending(b => b.BorrowDate)
            .ToListAsync();
    }

    public async Task UpdateAsync(BorrowRecord record)
    {
        _context.BorrowRecords.Update(record);
        await _context.SaveChangesAsync();
    }
}