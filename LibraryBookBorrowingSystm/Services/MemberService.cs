using LibraryBookBorrowingSystm.DTOs.Requests;
using LibraryBookBorrowingSystm.DTOs.Responses;
using LibraryBookBorrowingSystm.Services.Interfaces;

namespace LibraryBookBorrowingSystm.Services;

// TODO: Member 2 — Implement MemberService
public class MemberService : IMemberService
{
    public Task<IEnumerable<MemberResponse>> GetAllAsync() => throw new NotImplementedException();
    public Task<MemberResponse> GetByIdAsync(Guid id) => throw new NotImplementedException();
    public Task<MemberResponse> AddAsync(CreateMemberRequest request) => throw new NotImplementedException();
    public Task<MemberResponse> UpdateAsync(Guid id, UpdateMemberRequest request) => throw new NotImplementedException();
    public Task DeleteAsync(Guid id) => throw new NotImplementedException();
}
