using LibraryBookBorrowingSystm.Models;

namespace LibraryBookBorrowingSystm.Repositories;

public interface IBorrowRecordRepository
{
    Task<BorrowRecord> AddAsync(BorrowRecord record);
    Task<BorrowRecord?> GetActiveBorrowRecordAsync(Guid memberId, Guid bookId);
    Task<IEnumerable<BorrowRecord>> GetHistoryForMemberAsync(Guid memberId);
    Task UpdateAsync(BorrowRecord record);
}