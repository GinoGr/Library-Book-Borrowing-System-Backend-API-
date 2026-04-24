
using LibraryBookBorrowingSystem.Models;
using LibraryBookBorrowingSystem.Repositories;

namespace LibraryBookBorrowingSystem.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;

    public MemberService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<IEnumerable<Member>> GetMembers()
    {
        return await _memberRepository.GetAllAsync();
    }

    public async Task<Member?> GetMemberByIdAsync(Guid id)
    {
        return await _memberRepository.GetByIdAsync(id);
    }

    public async Task<Member?> GetMemberByEmailAsync(string email)
    {
        return await _memberRepository.GetByEmailAsync(email);
    }

    public async Task<Member?> GetMemberByNameAsync(string name)
    {
        return await _memberRepository.GetByNameAsync(name);
    }

    public async Task<Member> CreateMemberAsync(Member member)
    {
        return await _memberRepository.CreateAsync(member);
    }

    public async Task<Member> UpdateMemberAsync(Guid id, Member member)
    {
        var existing = await _memberRepository.GetByIdAsync(id);
        if (existing == null)
            throw new InvalidOperationException("Member not found.");

        existing.FullName = member.FullName;
        existing.Email = member.Email;

        return await _memberRepository.UpdateAsync(existing);
    }

    public async Task DeleteMemberAsync(Guid id)
    {
        var existing = await _memberRepository.GetByIdAsync(id);
        if (existing == null)
            throw new InvalidOperationException("Member not found.");

        await _memberRepository.DeleteAsync(id);
    }
}