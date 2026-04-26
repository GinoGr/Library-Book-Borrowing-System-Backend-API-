using LibraryBookBorrowingSystm.Models;

namespace LibraryBookBorrowingSystm.Repositories.Interfaces;

public interface IMemberRepository
{
    Task<IEnumerable<Member>> GetAllAsync();
    Task<Member?> GetByIdAsync(Guid id);
    Task<Member?> GetByEmailAsync(string email);
    Task AddAsync(Member member);
    Task UpdateAsync(Member member);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
