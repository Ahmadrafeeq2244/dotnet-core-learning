using Microsoft.EntityFrameworkCore;
using my_books.Data.Models;

namespace my_books.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book_Author>()
                .HasOne(b=>b.Book)
                .WithMany(ba=>ba.Book_Authors)
                .HasForeignKey(ba=>ba.BookId);

            modelBuilder.Entity<Book_Author>()
                .HasOne(b => b.Author)
                .WithMany(ba => ba.Book_Authors)
                .HasForeignKey(ba => ba.AuthorId);


           modelBuilder.Entity<Log>()
                .HasKey(N => N.Id);
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Book_Author> Book_Author { get; set; }
        public DbSet<Publisher> Publisher { get; set; }


        public DbSet<Log> Logs {get; set; }
    }
}
