using LibraryBookBorrowingSystm.DTOs.Requests;
using LibraryBookBorrowingSystm.DTOs.Responses;
using LibraryBookBorrowingSystm.Services.Interfaces;

namespace LibraryBookBorrowingSystm.Services;

// TODO: Member 3 — Implement BorrowService
public class BorrowService : IBorrowService
{
    public Task<IEnumerable<BorrowRecordResponse>> GetAllAsync() => throw new NotImplementedException();
    public Task<IEnumerable<BorrowRecordResponse>> GetByMemberIdAsync(Guid memberId) => throw new NotImplementedException();
    public Task<BorrowRecordResponse> BorrowAsync(BorrowBookRequest request) => throw new NotImplementedException();
    public Task<BorrowRecordResponse> ReturnAsync(Guid id) => throw new NotImplementedException();
}
