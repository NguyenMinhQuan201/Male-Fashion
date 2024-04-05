using Domain.Features;
using Domain.Features.Supplier;
using Domain.Features.Supplier.Dto;
using Domain.Models.Dto.ImportInvoice;
using Domain.Models.Dto.ImportInvoiceDto;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportInvoiceController : ControllerBase
    {
        private readonly IImportInvoiceService _importInvoiceService;
        private readonly IImportInvoiceDetailsService _importInvoiceDetailsService;

        public ImportInvoiceController(IImportInvoiceDetailsService importInvoiceDetailsService, IImportInvoiceService importInvoiceService)
        {
            _importInvoiceDetailsService = importInvoiceDetailsService;
            _importInvoiceService = importInvoiceService;
        }
        [HttpPost("add-importInvoice")]
        public async Task<IActionResult> Create([FromBody] ImportInvoiceDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _importInvoiceService.Create(request);
                if (result.IsSuccessed)
                {
                    return Ok(result.IsSuccessed);
                }
            }
            return BadRequest();
        }
        [HttpPut("update-importInvoice")]
        public async Task<IActionResult> Update(int id, [FromBody] ImportInvoiceDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _importInvoiceService.Update(id, request);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }

            }
            return BadRequest();
        }
        [HttpDelete("delete-importInvoice")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _importInvoiceService.Delete(id);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }
            }
            return BadRequest();
        }
        [HttpGet("get-by-name-importInvoice")]
        public async Task<IActionResult> GetByName(int? pageSize, int? pageIndex, string? name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _importInvoiceService.GetByName(pageSize, pageIndex, name);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
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
                var result = await _importInvoiceService.GetById(id);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }
            }
            return BadRequest();
        }
        //Detail
        [HttpPost("add-importInvoiceDetail")]
        public async Task<IActionResult> CreateImportInvoiceDetail([FromBody] ImportInvoiceDetailsDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _importInvoiceDetailsService.Create(request);
                if (result.IsSuccessed)
                {
                    return Ok(result.IsSuccessed);
                }
            }
            return BadRequest();
        }
        [HttpPut("update-importInvoiceDetail")]
        public async Task<IActionResult> UpdateImportInvoiceDetail(int id, [FromBody] ImportInvoiceDetailsDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _importInvoiceDetailsService.Update(id, request);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }

            }
            return BadRequest();
        }
        [HttpDelete("delete-importInvoiceDetail")]
        public async Task<IActionResult> DeleteImportInvoiceDetail(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _importInvoiceDetailsService.Delete(id);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }
            }
            return BadRequest();
        }
        [HttpGet("get-by-name-importInvoiceDetail")]
        public async Task<IActionResult> GetByNameInvoiceDetail(int? pageSize, int? pageIndex, string? name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _importInvoiceDetailsService.GetByName(pageSize, pageIndex, name);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }

            }

            return BadRequest();
        }
        [HttpGet("get-by-id-importInvoiceDetail")]
        public async Task<IActionResult> GetByIdInvoiceDetail(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _importInvoiceDetailsService.GetById(id);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }
            }
            return BadRequest();
        }
    }
}
