using my_books.Data.Models;
using my_books.Data.ViewModels;

namespace my_books.Data.Services
{
    public class AuthorService
    {
        private AppDbContext _context;

        public AuthorService(AppDbContext context)
        {
            _context = context;

        }
        public void AddAuthor(AuthorVM author)
        {
            var _author = new Author()
            {
                FullName = author.FullName
            };
            _context.Author.Add(_author);
            _context.SaveChanges();
        }

        public AuthorWithBookVM GetAuthorWithBooks(int authorId)
        {

            var _authorwithBooks = _context.Author.Where(n=>n.Id== authorId).Select(author => new AuthorWithBookVM()
            {
                FullName = author.FullName,
                books = author.Book_Authors.Select(n => n.Book.Title).ToList()
            }).FirstOrDefault();

            return _authorwithBooks;
        }


    }
}
