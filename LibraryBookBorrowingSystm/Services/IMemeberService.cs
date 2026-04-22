using LibraryBookBorrowingSystem.Models;

namespace LibraryBookBorrowingSystem.Services;

public interface IMemberService
{
    IEnumerable<Member> GetMembers();
    Member? GetMemberById(Guid id);
    Member? GetMemberByName(string firstName, string lastName); 
    Member? GetMemberByEmail(string email);
    Member CreateMember(Member request);

    Member UpdateMember(Guid id, Member request);
    void DeleteMember(Guid id);
}