using Microsoft.EntityFrameworkCore;
using LibraryBookBorrowingSystem.Models;

namespace LibraryBookBorrowingSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Member> Members => Set<Member>();
//        public DbSet<Registration> Registrations => Set<Registration>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Member>().ToTable("Members");
        }
    }
}