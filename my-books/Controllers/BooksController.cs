using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModels;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        public BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }


        [HttpGet("get-all-books")]

        public IActionResult getAllBooks()
        {
            var books= _bookService.GetAllBooks();
            return Ok(books);
        }

        [HttpGet("get-book-by-id/{id}")]
        public IActionResult getbookbyId(int id)
        {
            var book = _bookService.GetBookById(id);
            return Ok(book);
        }

        [HttpPost("add-book")]

        public IActionResult AddBook([FromBody]BookVM book)
        {
            _bookService.AddBookWithAuthor(book);
            return Ok();
        }

        [HttpPut("update-book-by-id/{id}")]

        public IActionResult UpdateBook(int id,BookVM book)
        {
           var UpdatedBook= _bookService.UpdateBookById(id, book);
            return Ok(UpdatedBook);
        }

        [HttpDelete("delete-book-by-id/{id}")]
        
        public IActionResult DeleteBookById(int id)
        {

            _bookService.DeleteBookById(id);
            return Ok();
        }
            
        }
}
