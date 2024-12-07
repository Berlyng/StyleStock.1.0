using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StyleStock.application.Interface;
using StyleStock.application.Service;
using StyleStock.common.DTOS;

namespace StyleStock.api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class SalesDetailController : ControllerBase
	{
		private readonly ISalesDetailService _salesDetailService;

		public SalesDetailController(ISalesDetailService salesDetailService)
		{
			_salesDetailService = salesDetailService;
		}

		[HttpGet(Name = "GetAllSalesDetail")]
		public async Task<IActionResult> GetAllSalesDetail()
		{
			try
			{
				var details = await _salesDetailService.GetAllSalesAsync();
				return Ok(details);
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{id}", Name = "GetSalesDetailById")]
		public async Task<IActionResult> GetSalesDetailById(int id)
		{
			try
			{
				var detail = await _salesDetailService.GetSaleByIdAsync(id);
				return Ok(detail);
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost(Name = "AddSaleDetail")]
		public async Task<IActionResult> AddSaleDetail(CreateSalesDetailDTO detailDto)
		{
			try
			{
				await _salesDetailService.AddSaleAsync(detailDto);
				return Ok(new { Message = "Detalle creado exitosamente", Data = detailDto });
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpPut("{id}", Name = "UpdateSaleDetail")]
		public async Task<IActionResult> UpdateSaleDetail(int id, UpdateSalesDetailDTO detailDto)
		{
			try
			{
				await _salesDetailService.UpdateSaleAsync(id, detailDto);
				return NoContent();
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete("{id}", Name = "DeleteSaleDetail")]
		public async Task<IActionResult> DeleteSaleDetail(int id)
		{
			try
			{
				await _salesDetailService.DeleteSaleAsync(id);
				return NoContent();
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

	}
}
