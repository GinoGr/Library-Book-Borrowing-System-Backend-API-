using System.ComponentModel.DataAnnotations;
using LibraryBookBorrowingSystem.Models;

namespace LibraryBookBorrowingSystem.Services;

public interface IMemberService
{
    Task<IEnumerable<Member>> GetMembers();
    Task<Member?> GetMemberByIdAsync(Guid id);
    Task<Member?> GetMemberByEmailAsync(string email);
    Task<Member?> GetMemberByNameAsync(string name);

    Task<Member> CreateMemberAsync(Member member);
    Task<Member> UpdateMemberAsync(Guid id, Member member);
    Task DeleteMemberAsync(Guid id);

}