using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StyleStock.application.Interface;
using StyleStock.common.DTOS;

namespace StyleStock.api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class SaleController : ControllerBase
	{
		private readonly ISaleService _saleService;

		public SaleController(ISaleService saleService)
		{
			_saleService = saleService;
		}

		[HttpGet(Name = "GetAllSale")]
		public async Task<IActionResult> GetAllSale()
		{
			try
			{
				var purchase = await _saleService.GetAllSalesAsync();
				return Ok(purchase);
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{id}", Name = "GetSaleById")]
		public async Task<IActionResult> GetSaleById(int id)
		{
			try
			{
				var product = await _saleService.GetSaleByIdAsync(id);
				return Ok(product);
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost(Name = "AddSale")]
		public async Task<IActionResult> AddSale(CreateSaleDTO saleDto)
		{
			try
			{
				await _saleService.AddSaleAsync(saleDto);
				return Ok(new { Message = "Venta realizada exitosamente", Data = saleDto });
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpPut("{id}", Name = "UpdateSale")]
		public async Task<IActionResult> UpdateSale(int id, UpdateSaleDTO saleDto)
		{
			try
			{
				await _saleService.UpdateSaleAsync(id, saleDto);
				return Ok(new { Message = "venta actualizada exitosamente", Data = saleDto });
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete("{id}", Name = "DeleteSale")]
		public async Task<IActionResult> DeleteSale(int id)
		{
			try
			{
				await _saleService.DeleteSaleAsync(id);
				return Ok("Eliminado exitosamente");
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}


	}
}
