using LibraryBookBorrowingSystem.Models;

namespace LibraryBookBorrowingSystem.Repositories;

public interface IMemberRepository
{
    List<Member> GetAll();
    Member? GetById(Guid id);
    Member? GetByName(String fullName);
    Member? GetByEmail(String email);
    Member Add(Member member);
    Member Update(Member member);
    void Delete(Member member);
}