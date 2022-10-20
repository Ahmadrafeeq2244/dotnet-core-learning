using my_books.Data.Models;
using my_books.Data.Paging;
using my_books.Data.ViewModels;
using my_books.Exceptions;
using System.Text.RegularExpressions;

namespace my_books.Data.Services
{
    public class PublisherService
    {

        private AppDbContext _context;

        public PublisherService(AppDbContext context)
        {
            _context = context;

        }
        public Publisher AddPublisher(PublisherVM publisher)
        {
            if (StringStartWithNumber(publisher.Name)) throw new PublisherNameException("Name Start With Number", publisher.Name);

            var _publisher = new Publisher()
            {
                Name = publisher.Name
            };
            _context.Publisher.Add(_publisher);
            _context.SaveChanges();

            return _publisher;
        }

        public Publisher GetPubByID(int id) => _context.Publisher.FirstOrDefault(n => n.Id == id);
        public PublisherWithBooksVM GetPublisherByID(int publisherId)
        {
            var _publisher = _context.Publisher.Where(n => n.Id == publisherId).Select(publsher => new PublisherWithBooksVM()
            {
                Name = publsher.Name,
                Book = publsher.Books.Select(bookAuthors => new BookAuthors()
                {
                    BookName = bookAuthors.Title,
                    Authors = bookAuthors.Book_Authors.Select(authors => authors.Author.FullName).ToList()
                }).ToList()
            }).FirstOrDefault();
            return _publisher;
        }

        public void DeletePublisherById(int id)
        {
            var publisher = _context.Publisher.FirstOrDefault(n => n.Id == id);
            if (publisher != null)
            {
                _context.Publisher.Remove(publisher);
                _context.SaveChanges();
        } 
        
        }

        public List<Publisher> GetAllPublishers(string sortby,string StringSearch,int? pageNumber)
        {
            var publisers = _context.Publisher.OrderBy(n=>n.Id).ToList();
            if(!string.IsNullOrEmpty(sortby))
            {
                switch(sortby)
                {
                    case "name_desc":
                        publisers = publisers.OrderByDescending(n => n.Name).ToList();
                        break;
                        default:
                        break;
                }
            }
            if(!string.IsNullOrEmpty(StringSearch))
            {
                publisers = publisers.Where(n => n.Name.Contains(StringSearch,StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            //Paging

            int pagesize = 5;
            publisers = PaginatedList<Publisher>.Create(publisers.AsQueryable(), pageNumber ?? 1, pagesize);

            
            return publisers;
        }

        private bool StringStartWithNumber(string name) => (Regex.IsMatch(name, @"^\d"));

    }
}
