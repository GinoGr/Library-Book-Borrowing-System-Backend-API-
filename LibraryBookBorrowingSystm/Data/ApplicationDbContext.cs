using Microsoft.EntityFrameworkCore;
using LibraryBookBorrowingSystem.Models;

namespace LibraryBookBorrowingSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Member> Members => Set<Member>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Member>().ToTable("Members");
        }
    }
}