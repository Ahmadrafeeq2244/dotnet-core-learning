using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace my_books.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiVersion("1.2")]
    [ApiVersion("1.9")]
     [Route("api/[controller]")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("get-test")]
        public IActionResult getTest()
        {
            return Ok("This is Test Version 1.0");
        }

        [HttpGet("get-test"),MapToApiVersion("1.2")]
        public IActionResult getTestv12()
        {
            return Ok("This is Test Version 1.2");
        }

        [HttpGet("get-test"),MapToApiVersion("1.9")]
        public IActionResult getTestv19()
        {
            return Ok("This is Test Version 1.9");
           
        }

    }
}
