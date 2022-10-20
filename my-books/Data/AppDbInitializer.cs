using my_books.Data.Models;

namespace my_books.Data
{
    public class AppDbInitializer
    {

        public static void seed(IApplicationBuilder applicationBuilder)
        {
            using(var servicescope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = servicescope.ServiceProvider.GetService<AppDbContext>();
                if(!context.Books.Any())
                {
                    context.Books.AddRange(new Book()
                    {
                        Title = "Book",
                        Description = "No Desc",
                        IsRead = true,
                        DateRead =Convert.ToDateTime("2021-01-01"),
                        Rate = 250,
                        Genre = "yes",
                        CoverUrl = "yes.com",
                        DateAdded = Convert.ToDateTime("2020-01-01"),
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
