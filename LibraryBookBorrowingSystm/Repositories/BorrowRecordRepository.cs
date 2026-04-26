using LibraryBookBorrowingSystm.Data;
using LibraryBookBorrowingSystm.Models;
using LibraryBookBorrowingSystm.Repositories.Interfaces;

namespace LibraryBookBorrowingSystm.Repositories;

// TODO: Member 3 — Implement BorrowRecordRepository
public class BorrowRecordRepository : IBorrowRecordRepository
{
    private readonly ApplicationDbContext _context;

    public BorrowRecordRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<BorrowRecord>> GetAllAsync() => throw new NotImplementedException();
    public Task<BorrowRecord?> GetByIdAsync(Guid id) => throw new NotImplementedException();
    public Task<IEnumerable<BorrowRecord>> GetByMemberIdAsync(Guid memberId) => throw new NotImplementedException();
    public Task AddAsync(BorrowRecord record) => throw new NotImplementedException();
    public Task<BorrowRecord?> GetActiveRecordAsync(Guid bookId, Guid memberId) => throw new NotImplementedException();
}
