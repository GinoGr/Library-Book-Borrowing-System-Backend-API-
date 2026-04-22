using LibraryBookBorrowingSystm.Data;
using LibraryBookBorrowingSystm.Models;
using LibraryBookBorrowingSystm.Repositories.Interfaces;

namespace LibraryBookBorrowingSystm.Repositories;

// TODO: Member 2 — Implement MemberRepository
public class MemberRepository : IMemberRepository
{
    private readonly ApplicationDbContext _context;

    public MemberRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Member>> GetAllAsync() => throw new NotImplementedException();
    public Task<Member?> GetByIdAsync(Guid id) => throw new NotImplementedException();
    public Task AddAsync(Member member) => throw new NotImplementedException();
    public Task UpdateAsync(Member member) => throw new NotImplementedException();
    public Task DeleteAsync(Guid id) => throw new NotImplementedException();
    public Task<bool> ExistsAsync(Guid id) => throw new NotImplementedException();
}
