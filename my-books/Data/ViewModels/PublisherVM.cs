namespace my_books.Data.ViewModels
{
    public class PublisherVM
    {
        public string Name { get; set; }
    }

    public class PublisherWithBooksVM
    {
        public string Name { get; set; }
        public List<BookAuthors> Book { get; set; }
    }

    public class BookAuthors
    {
        public string BookName { get; set; }

        public List<string> Authors { get; set; }
    }

}
