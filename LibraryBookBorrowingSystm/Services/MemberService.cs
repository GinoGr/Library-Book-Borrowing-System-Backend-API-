using LibraryBookBorrowingSystm.DTOs.Requests;
using LibraryBookBorrowingSystm.DTOs.Responses;
using LibraryBookBorrowingSystm.Exceptions;
using LibraryBookBorrowingSystm.Models;
using LibraryBookBorrowingSystm.Repositories.Interfaces;
using LibraryBookBorrowingSystm.Services.Interfaces;

namespace LibraryBookBorrowingSystm.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;

    public MemberService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<IEnumerable<MemberResponse>> GetAllAsync()
    {
        var members = await _memberRepository.GetAllAsync();
        return members.Select(MapToResponse).ToList();
    }

    public async Task<MemberResponse> GetByIdAsync(Guid id)
    {
        var member = await _memberRepository.GetByIdAsync(id);
        if (member == null) throw new NotFoundException($"Member with ID {id} was not found.");

        return MapToResponse(member);
    }

    public async Task<MemberResponse> AddAsync(CreateMemberRequest request)
    {
        if (await _memberRepository.GetByEmailAsync(request.Email) != null)
            throw new ConflictException("Email is already in use.");

        var member = new Member
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName,
            Email = request.Email,
            MembershipDate = DateTimeOffset.UtcNow
        };

        await _memberRepository.AddAsync(member);
        return MapToResponse(member);
    }

    public async Task<MemberResponse> UpdateAsync(Guid id, UpdateMemberRequest request)
    {
        var member = await _memberRepository.GetByIdAsync(id);
        if (member == null) throw new NotFoundException($"Member with ID {id} was not found.");

        var memberWithEmail = await _memberRepository.GetByEmailAsync(request.Email);
        if (memberWithEmail != null && memberWithEmail.Id != id)
            throw new ConflictException("Email is already in use.");

        member.FullName = request.FullName;
        member.Email = request.Email;

        await _memberRepository.UpdateAsync(member);
        return MapToResponse(member);
    }

    public async Task DeleteAsync(Guid id)
    {
        if (!await _memberRepository.ExistsAsync(id))
            throw new NotFoundException($"Member with ID {id} was not found.");

        await _memberRepository.DeleteAsync(id);
    }

    private static MemberResponse MapToResponse(Member member) => new MemberResponse
    {
        Id = member.Id,
        FullName = member.FullName,
        Email = member.Email,
        MembershipDate = member.MembershipDate
    };
}
