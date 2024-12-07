using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StyleStock.application.Interface;
using StyleStock.application.Service;
using StyleStock.common.DTOS;

namespace StyleStock.api.Controllers
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class PurchaseDetailController : ControllerBase
	{
		private readonly IPurchaseDetailService _purchaseDetailService;

		public PurchaseDetailController(IPurchaseDetailService purchaseDetailService)
		{
			_purchaseDetailService = purchaseDetailService;
		}

		[HttpGet(Name = "GetAllPurchaseDetail")]
		public async Task<IActionResult> GetAllPurchaseDetail()
		{
			try
			{
				var details = await _purchaseDetailService.GetAllPurchaseDetailAsync();
				return Ok(details);
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{id}", Name = "GetPurchaseDetailById")]
		public async Task<IActionResult> GetPurchaseDetailById(int id)
		{
			try
			{
				var detail = await _purchaseDetailService.GetPurchaseDetailByIdAsync(id);
				return Ok(detail);
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost(Name = "AddPurchaseDetail")]
		public async Task<IActionResult> AddPurchaseDetail(CreatePurchaseDetailDTO detailDto)
		{
			try
			{
				await _purchaseDetailService.AddPurchaseDetailAsycn(detailDto);
				return Ok(new { Message = "Detalle creado exitosamente", Data = detailDto });
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpPut("{id}", Name = "UpdatePurchaseDetail")]
		public async Task<IActionResult> UpdatePurchaseDetail(int id, UpdatePurchaseDetailDTO detailDto)
		{
			try
			{
				await _purchaseDetailService.UpdatePurchaseDetailAsync(id, detailDto);
				return NoContent();
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete("{id}", Name = "DeletePurchaseDetail")]
		public async Task<IActionResult> DeletePurchaseDetail(int id)
		{
			try
			{
				await _purchaseDetailService.DeletePurchaseDetailAsync(id);
				return NoContent();
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}
	}

}
