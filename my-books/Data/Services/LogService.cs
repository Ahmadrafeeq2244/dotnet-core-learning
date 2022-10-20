using my_books.Data.Models;

namespace my_books.Data.Services
{
    public class LogService
    {
        private AppDbContext _context;

        public LogService(AppDbContext context)
        {
            _context = context;

        }

        public List<Log> getallLogs()
        {
            var allLogs = _context.Logs.ToList();
            return allLogs;
        }
    }
}
