using LibraryBookBorrowingSystem.Models;

namespace LibraryBookBorrowingSystem.Repositories;

public interface IMemberRepository
{
    List<Member> GetAll();
    Member? GetById(Guid id);
    Member? GetByName(String Fname, String Lname);
    Member? GetByEmail(String email);
    Member Add(Member member);
}