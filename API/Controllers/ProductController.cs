using Domain.Features.Product;
using Domain.Models.Dto.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;

        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductImage request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.Create(request);
            if (result == 0) return BadRequest();
            /*var product = await _productService.GetById(result, request.LanguageId);
            return CreatedAtAction(nameof(GetById), new { id = result }, product);*/
            return Ok(result);
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.GetById(id,"string");
            if (result == null) return BadRequest();
            
            return Ok(result);
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll(int? pageSize, int? pageIndex, string?search)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.GetAllPaging( pageSize, pageIndex, search);
            if (result==null) return BadRequest();
            return Ok(result);
        }
    }
}
