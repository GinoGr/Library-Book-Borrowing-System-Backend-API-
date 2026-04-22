using LibraryBookBorrowingSystem.Data;
using LibraryBookBorrowingSystem.Models;

namespace LibraryBookBorrowingSystem.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly ApplicationDbContext _context;

    public MemberRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Member> GetAll()
    {
        return _context.Members.ToList();
    }

    public Member? GetById(Guid id)
    {
        return _context.Members.Find( id);
    }

    public Member? GetByName(String fullName)
    {
        return _context.Members.FirstOrDefault(u => u.FullName == fullName);
    }
    public Member? GetByEmail(String email)
    {
        return _context.Members.FirstOrDefault(u => u.Email == email);
    }

    public Member Add(Member member)
    {
        _context.Members.Add(member);
        _context.SaveChanges();
        return member;
    }

    public Member Update(Member member)
    {
        _context.Members.Update(member);
        _context.SaveChanges();
        return member;
    }

    public void Delete(Member member)
    {
        _context.Members.Remove(member);
        _context.SaveChanges();
    }
}