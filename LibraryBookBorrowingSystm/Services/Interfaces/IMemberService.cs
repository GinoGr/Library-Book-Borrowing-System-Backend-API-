using LibraryBookBorrowingSystm.DTOs.Requests;
using LibraryBookBorrowingSystm.DTOs.Responses;

namespace LibraryBookBorrowingSystm.Services.Interfaces;

public interface IMemberService
{
    Task<IEnumerable<MemberResponse>> GetAllAsync();
    Task<MemberResponse> GetByIdAsync(Guid id);
    Task<MemberResponse> AddAsync(CreateMemberRequest request);
    Task<MemberResponse> UpdateAsync(Guid id, UpdateMemberRequest request);
    Task DeleteAsync(Guid id);
}
