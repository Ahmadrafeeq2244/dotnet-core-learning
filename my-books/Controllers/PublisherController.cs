using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModels;
using my_books.Exceptions;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {

        public PublisherService _publisherService;
        private readonly ILogger <PublisherController> _logger;

        public PublisherController(PublisherService publisherService, ILogger<PublisherController> logger)
        {
            _publisherService = publisherService;
            _logger = logger;
        }

        [HttpPost("add-publisher")]

        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            try
            {
                int a = 0;
               // int x = 12 / a;
                var _publisher = _publisherService.AddPublisher(publisher);
                return Created(nameof(AddPublisher), _publisher);
            }catch(PublisherNameException ex)
            {
                return BadRequest($"{ex.Message}, Publisher Name :{ex.PublisherName}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-all-publisher")]

        public IActionResult getallPublisher(string? sortby, string? StringSearch,int? pageNumber)
        {
         //   throw new Exception("This Exception Thrown BY get ALl Publishher");
            try
            {
                _logger.LogInformation("This is just a log in Get ALL Publisher");
                var _result = _publisherService.GetAllPublishers(sortby,StringSearch, pageNumber);
                return Ok(_result);
            }
            catch
            {
                return BadRequest("Error While Fetching Publishers");
            }
        }


        [HttpGet("get-publisher-by_Id/{Id}")]
        public IActionResult getpubBYID(int Id)
        {
            // throw new Exception("This is an Exception that will be handled by middleware");
            int n = 0;
          //  int c = 12 / n;
            var publisher = _publisherService.GetPubByID(Id);
            if(publisher!=null)
            {
                return Ok(publisher);

            }else
            {
                return NotFound();
            }
        }

        [HttpGet("get-publisher_WithBook-by-Id/{Id}")]
        public IActionResult getPublisherById(int Id)
        {
            var publisher = _publisherService.GetPublisherByID(Id);
            return Ok(publisher);
        }
        [HttpDelete("Delete-Publisher-By-Id/{Id}")]
        public IActionResult DeletePublisherById(int Id)
        {
            _publisherService.DeletePublisherById(Id);
            return Ok();
        }
    }
}
