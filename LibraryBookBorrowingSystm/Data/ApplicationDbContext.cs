using LibraryBookBorrowingSystm.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryBookBorrowingSystm.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Book> Books { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<BorrowRecord> BorrowRecords { get; set; }
}
