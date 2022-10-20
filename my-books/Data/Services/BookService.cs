using my_books.Data.Models;
using my_books.Data.ViewModels;
using my_books.Migrations;
using System.Threading;

namespace my_books.Data.Services
{
    public class BookService
    {
        private AppDbContext _context;
     
        public BookService(AppDbContext context)
        {
            _context = context;

        }
        public void AddBookWithAuthor(BookVM book)
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                var _book = new Book()
                {
                    Title = book.Title,
                    Description = book.Description,
                    IsRead = book.IsRead,
                    DateRead = book.IsRead ? book.DateRead : null,
                    Rate = book.IsRead ? book.Rate : null,
                    Genre = book.Genre,
                    CoverUrl = book.CoverUrl,
                    DateAdded = DateTime.Now,
                    PublisherId = book.PublisherId
                };
                _context.Books.Add(_book);
                _context.SaveChanges();

                foreach (var id in book.AuthorIds)
                {
                    var _bookAuthor = new Book_Author()
                    {
                        BookId = _book.Id,
                        AuthorId = id
                    };
                    _context.Book_Author.Add(_bookAuthor);
                    _context.SaveChanges();
                }
                transaction.Commit();
            }
            catch(Exception ex)
            {
                transaction.Rollback();
            }
        }

        public List<Book> GetAllBooks() => _context.Books.ToList();

        public BookWithAuthorVM GetBookById(int bookId)
        {
            var _bookwithAuthor = _context.Books.Where(n=>n.Id==bookId).Select(book => new BookWithAuthorVM()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead : null,
                Rate = book.IsRead ? book.Rate : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                PublisherName = book.Publisher.Name,
                Authors=book.Book_Authors.Select(n=>n.Author.FullName).ToList()
            }
            ).FirstOrDefault();

            return _bookwithAuthor;
        }

        public Book UpdateBookById(int bookid,BookVM book)
        {
            var _book = _context.Books.FirstOrDefault(n => n.Id == bookid);
              if (_book != null)
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.IsRead = book.IsRead;
                _book.DateRead = book.IsRead ? book.DateRead : null;
                _book.Rate = book.IsRead ? book.Rate : null;
                _book.Genre = book.Genre;
                _book.CoverUrl = book.CoverUrl;
                _context.SaveChanges();
            }
            return _book;
        }

        public void DeleteBookById(int id)
        {
            var _book = _context.Books.FirstOrDefault(n => n.Id == id);
            if(_book!=null)
            {
                _context.Books.Remove(_book);
                _context.SaveChanges();
            }
        }

    }
}
