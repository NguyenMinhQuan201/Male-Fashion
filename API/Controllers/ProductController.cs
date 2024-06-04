using Domain.Features;
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
        [HttpPost("post")]
        public async Task<IActionResult> Create([FromForm] ProductDto request)
        {
            if (!ModelState.IsValid || request.Description==null)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.Create(request);
            if (result.IsSuccessed==false) return BadRequest(result.Message);

            return Ok();
        }
        [HttpPost("post-detail")]
        public async Task<IActionResult> CreateDetail([FromBody] ProductDetailDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.CreateDetailProduct(request);
            if (result.IsSuccessed == false) return BadRequest();
            return Ok(result);
        }
        [HttpPut("put")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDto request)
        {
            if (!ModelState.IsValid )
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.UpdateProductWithoutImage(id,request);
            if (result.IsSuccessed==false) return BadRequest();
            return Ok(result);
        }
        [HttpPut("put-detail")]
        public async Task<IActionResult> UpdateDetail(int id, [FromBody] ProductDetailDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.UpdateDetail(id, request);
            if (result.IsSuccessed == false) return BadRequest();
            return Ok(result);
        }
        [HttpDelete("remove")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid )
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.Delete(id);
            if (result.IsSuccessed == false) return BadRequest();
            return Ok(result);
        }
        [HttpDelete("remove-detail")]
        public async Task<IActionResult> DeleteDetail(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.DeleteDetail(id);
            if (result.IsSuccessed == false) return BadRequest();
            return Ok(result);
        }
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.GetById(id);
            if (result == null) return BadRequest();
            
            return Ok(result);
        }
        [HttpGet("get-by-id-detail")]
        public async Task<IActionResult> GetByIdDetail(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.GetByIdDetail(id);
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
        [HttpGet("get-all-no-paging")]
        public async Task<IActionResult> GetAllWithotPaging()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.GetAll();
            if (result == null) return BadRequest();
            return Ok(result);
        }
        [HttpGet("get-all-detail")]
        public async Task<IActionResult> GetAllDetail(int? pageSize, int? pageIndex, string? search)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.GetAllPagingDetail(pageSize, pageIndex, search);
            if (result == null) return BadRequest();
            return Ok(result);
        }
        [HttpGet("get-all-detail-removed")]
        public async Task<IActionResult> GetAllDetailRemoved(int? pageSize, int? pageIndex, string? search)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.GetAllPagingDetailRemoved(pageSize, pageIndex, search);
            if (result == null) return BadRequest();
            return Ok(result);
        }
        [HttpGet("get-all-removed")]
        public async Task<IActionResult> GetAllProductRemoved(int? pageSize, int? pageIndex, string? search)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.GetAllPagingRemoved(pageSize, pageIndex, search);
            if (result == null) return BadRequest();
            return Ok(result);
        }
        [HttpPost("add-image")]
        public async Task<IActionResult> AddImage([FromForm] AddImageRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _productService.AddImage(request.Id, request.ProductImageVMs);

                if (result.IsSuccessed != true)
                {
                    return BadRequest();
                }

                return Ok(result);
            }
        }
        [HttpDelete("remove-image")]
        public async Task<IActionResult> RemoveImage(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _productService.RemoveImage(id);

                if (result == 0)
                {
                    return BadRequest();
                }

                return Ok();
            }
        }
        [HttpDelete("un-remove")]
        public async Task<IActionResult> UnDelete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.RestoreProduct(id);
            if (result.IsSuccessed == false) return BadRequest();
            return Ok(result);
        }
        [HttpDelete("un-remove-detail")]
        public async Task<IActionResult> UnDeleteDetail(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.RestoreProductDetail(id);
            if (result.IsSuccessed == false) return BadRequest();
            return Ok(result);
        }
        [HttpGet("get-all-detail-with-productid")]
        public async Task<IActionResult> GetAllDetailByIdPoduct(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.GetAllDetailByIdPoduct(id);
            if (result.IsSuccessed == false) return BadRequest();
            return Ok(result);
        }
        [HttpGet("get-all-with-categoryId")]
        public async Task<IActionResult> GetAllProductByCate(int? pageSize, int? pageIndex,int?id, string? search,string ? branding, long priceMin, long priceMax)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.GetAllbyCategoryId(pageSize, pageIndex, id, search,branding,priceMin,priceMax);
            if (result.IsSuccessed == false) return BadRequest();
            return Ok(result);
        }
        [HttpGet("get-all-hot")]
        public async Task<IActionResult> GetAllProductHot()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.GetAllHot();
            return Ok(result);
        }
    }
}
