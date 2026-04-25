using LibraryBookBorrowingSystm.DTOs;

namespace LibraryBookBorrowingSystm.Services;

public interface IBorrowingService
{
    Task<BorrowRecordResponseDto> BorrowBookAsync(BorrowRequestDto request);
    Task<BorrowRecordResponseDto> ReturnBookAsync(Guid memberId, Guid bookId);
    Task<IEnumerable<BorrowRecordResponseDto>> GetMemberHistoryAsync(Guid memberId);
}