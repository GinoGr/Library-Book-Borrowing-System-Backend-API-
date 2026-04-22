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

    public IEnumerable<Member> GetMembers()
    {
        return _memberRepository.GetAll();
    }

    public Member? GetMemberById(Guid id)
    {
        return _memberRepository.GetById(id);
    }
    public Member? GetMemberByName(string firstName, string lastName)
    {
        return _memberRepository.GetByName(firstName, lastName);
    }
    public Member? GetMemberByEmail(string email)
    {
        return _memberRepository.GetByEmail(email);
    }
    public Member CreateMember(Member request)
    {
        var member = new Member
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            MembershipDate = DateTime.UtcNow,
        }; 

        return _memberRepository.Add(member);
    }

    public Member UpdateMember(Guid id, Member request)
    {
        var member = _memberRepository.GetById(id);
        if (member is null)
        {
            throw new InvalidOperationException("Member not found.");
        }

        member.FirstName = request.FirstName;
        member.LastName = request.LastName;
        member.Email = request.Email;

        return _memberRepository.Update(member);
    }

    public void DeleteMember(Guid id)
    {
        var member = _memberRepository.GetById(id);
        if (member is null)
        {
            throw new InvalidOperationException("Member not found.");
        }

        _memberRepository.Delete(member);
    }
}