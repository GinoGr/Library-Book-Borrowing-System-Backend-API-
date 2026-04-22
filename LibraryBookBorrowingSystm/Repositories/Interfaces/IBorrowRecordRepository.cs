using LibraryBookBorrowingSystm.Models;

namespace LibraryBookBorrowingSystm.Repositories.Interfaces;

public interface IBorrowRecordRepository
{
    Task<IEnumerable<BorrowRecord>> GetAllAsync();
    Task<BorrowRecord?> GetByIdAsync(Guid id);
    Task<IEnumerable<BorrowRecord>> GetByMemberIdAsync(Guid memberId);
    Task AddAsync(BorrowRecord record);
    Task<BorrowRecord?> GetActiveRecordAsync(Guid bookId, Guid memberId);
}
