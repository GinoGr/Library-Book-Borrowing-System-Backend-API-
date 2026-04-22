using LibraryBookBorrowingSystm.DTOs.Requests;
using LibraryBookBorrowingSystm.DTOs.Responses;

namespace LibraryBookBorrowingSystm.Services.Interfaces;

public interface IBorrowService
{
    Task<IEnumerable<BorrowRecordResponse>> GetAllAsync();
    Task<IEnumerable<BorrowRecordResponse>> GetByMemberIdAsync(Guid memberId);
    Task<BorrowRecordResponse> BorrowAsync(BorrowBookRequest request);
    Task<BorrowRecordResponse> ReturnAsync(Guid id);
}
