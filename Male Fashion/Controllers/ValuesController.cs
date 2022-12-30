using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Male_Fashion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("Call911")]
        public IActionResult De()
        {
            return Ok("OKOKOK");
        }
    }
}
