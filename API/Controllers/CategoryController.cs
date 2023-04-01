using Domain.Features;
using Domain.Models.Dto.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpPost("add-category")]
        public async Task<IActionResult> Create([FromBody] CategoryRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _categoryService.Create(request);
                if (result.IsSuccessed)
                {
                    return Ok(result.IsSuccessed);
                }
            }
            return BadRequest();
        }
        [HttpPut("update-category")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _categoryService.Update(id, request);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }

            }
            return BadRequest();
        }
        [HttpDelete("delete-category")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _categoryService.Delete(id);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }
            }
            return BadRequest();
        }
        [HttpGet("get-by-name-category")]
        public async Task<IActionResult> GetByname(int? pageSize, int? pageIndex, string? name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _categoryService.GetAll(pageSize, pageIndex, name);
                if (result.IsSuccessed)
                {
                    return Ok(result);
                }

            }

            return BadRequest();
        }
        [HttpGet("get-by-name-category-removed")]
        public async Task<IActionResult> GetBynameRemoved(int? pageSize, int? pageIndex, string? name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _categoryService.GetDeletedCategories(pageSize, pageIndex, name);
                if (result.IsSuccessed)
                {
                    return Ok(result);
                }

            }

            return BadRequest();
        }
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _categoryService.GetById(id);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }
            }
            return BadRequest();
        }
        [HttpGet("get-full")]
        public async Task<IActionResult> GetFull()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _categoryService.GetAll();
                return Ok(result);
            }
        }
        [HttpDelete("unremove")]
        public async Task<IActionResult> UnDelete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _categoryService.Restore(id);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }
            }
            return BadRequest();
        }
    }
}
