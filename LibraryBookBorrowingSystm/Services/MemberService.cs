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
            //Registrations = new List<Registration>()
        }; 

        return _memberRepository.Add(member);
    }
}