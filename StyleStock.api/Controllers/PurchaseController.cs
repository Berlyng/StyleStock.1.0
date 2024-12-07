using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StyleStock.application.Interface;
using StyleStock.application.Service;
using StyleStock.common.DTOS;

namespace StyleStock.api.Controllers
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class PurchaseController : ControllerBase
	{
		private readonly IPurchaseService _purchaseService;

		public PurchaseController(IPurchaseService purchaseService)
		{
			_purchaseService = purchaseService;
		}
		[HttpGet(Name = "GetAllPurchase")]
		public async Task<IActionResult> GetAllPurchase()
		{
			try
			{
				var purchase = await _purchaseService.GetAllPurchaseAsync();
				return Ok(purchase);
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{id}", Name = "GetPurchaseById")]
		public async Task<IActionResult> GetPurchaseById(int id)
		{
			try
			{
				var product = await _purchaseService.GetPurchaseByIdAsync(id);
				return Ok(product);
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost(Name = "AddPurchase")]
		public async Task<IActionResult> AddPurchase(CreatePurchaseDTO purchaseDto)
		{
			try
			{
				await _purchaseService.AddPurchaseAsycn(purchaseDto);
				return Ok(new { Message = "Pedido creado exitosamente", Data = purchaseDto });
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpPut("{id}", Name = "UpdatePurchase")]
		public async Task<IActionResult> UpdatePurchase(int id, UpdatePurchaseDTO purchaseDto)
		{
			try
			{
				await _purchaseService.UpdatePurchaseAsync(id, purchaseDto);
				return Ok(new { Message = "Pedido actuallizado exitosamente", Data = purchaseDto });
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete("{id}", Name = "DeletePurchase")]
		public async Task<IActionResult> DeletePurchase(int id)
		{
			try
			{
				await _purchaseService.DeletePurchaseAsync(id);
				return Ok("Eliminado exitosamente");
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}
	}

}



