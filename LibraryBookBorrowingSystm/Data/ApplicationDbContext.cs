using Microsoft.EntityFrameworkCore;
using LibraryBookBorrowingSystm.Models;

namespace LibraryBookBorrowingSystm.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Member> Members => Set<Member>();
	public DbSet<Book> Books => Set<Book>();
	public DbSet<BorrowRecord> BorrowRecords => Set<BorrowRecord>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Member>().ToTable("Members");
	    modelBuilder.Entity<Book>().ToTable("Books");
	    modelBuilder.Entity<BorrowRecord>().ToTable("BorrowRecords");
        }
    }
}