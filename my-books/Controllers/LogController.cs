using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {

        public LogService _logService;
       

        public LogController(LogService logService)
        {
            _logService = logService;
            
        }

        [HttpGet("get-all-logs")]

        public IActionResult getallLogs()
        {
            var logs = _logService.getallLogs();
            return Ok(logs);
        }
    }
}
