using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ValuesController : ControllerBase
    {
        [HttpGet("Call911")]
        public  IActionResult Call()
        {
            var x = new TestTClass()
            {
                Id = 1,
            };
            var lst = new List<TestTClass>();
            lst.Add(x);
            var y = new Mylist<TestTClass>()
            {
                
            };
            y.List = lst;
            Console.WriteLine(y);
            return Ok(y);

        }
    }
}
