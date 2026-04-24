using LibraryBookBorrowingSystem.Models;

namespace LibraryBookBorrowingSystem.Repositories;

public interface IMemberRepository
{
    Task<IEnumerable<Member>> GetAllAsync();
    Task<Member?> GetByIdAsync(Guid id);
    Task<Member?> GetByEmailAsync(string email);
    Task<Member?> GetByNameAsync(string name);
    Task<Member> CreateAsync(Member member);
    Task<Member> UpdateAsync(Member member);
    Task DeleteAsync(Guid id);
}