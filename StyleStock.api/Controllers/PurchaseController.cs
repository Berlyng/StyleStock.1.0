using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StyleStock.Common.DTOS;
using StyleStock.domain.Entities;
using StyleStock.domain.Repository;

namespace StyleStock.api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class PurchaseController : ControllerBase
	{
		private readonly IPurchaseRepository _purchaseRepo;
		private readonly IPurchaseDetailRepository _detailRepo;

		public PurchaseController(IPurchaseRepository purchaseRepo, IPurchaseDetailRepository detailRepo)
		{
			_purchaseRepo = purchaseRepo;
			_detailRepo = detailRepo;
		}

		[HttpGet(Name = "GetPurchase")]
		public async Task<IActionResult> GetPurchase()
		{
			try
			{
				var purchases = await _purchaseRepo.GetAllAsync();
				return Ok(purchases);
			}
			catch (Exception)
			{

				return StatusCode(500, "Ocurrió un error inesperado al obtener los Productos.");
			}
		}

		[HttpGet("WithDetails", Name = "GetAllPurchasesWithDetails")]
		public async Task<IActionResult> GetAllPurchasesWithDetails()
		{
			try
			{
		
				var purchases = await _purchaseRepo.GetAllAsync();
				if (purchases == null || !purchases.Any())
				{
					return NotFound("No se encontraron pedidos.");
				}


				var purchasesWithDetails = new List<object>();

				foreach (var purchase in purchases)
				{
					
					var details = await _detailRepo.GetByPurchaseIdAsync(purchase.Id);

					
					purchasesWithDetails.Add(new
					{
						Purchase = purchase,
						Details = details
					});
				}

				return Ok(purchasesWithDetails);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Ocurrió un error inesperado al obtener los pedidos: {ex.Message}");
			}
		}


		[HttpPost(Name = "CreatePurchaseWithDetails")]
		public async Task<IActionResult> CreatePurchaseWithDetails(CreatePurchaseDTO purchaseDto)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				var newPurchase = new Purchase
				{
					PurchaseDate = purchaseDto.PurchaseDate,
					SupplierID = purchaseDto.SupplierID,
					UserId = purchaseDto.UserId,
				};

				await _purchaseRepo.AddAsync(newPurchase);

	
				decimal totalAmount = 0;
				foreach (var detailDto in purchaseDto.PurchaseDetails)
				{
					var detail = new PurchaseDetail
					{
						PurchaseID = newPurchase.Id,
						ProductID = detailDto.ProductID,
						Quantity = detailDto.Quantity,
						UnitPrice = detailDto.UnitPrice,
						SubTotal = detailDto.Quantity * detailDto.UnitPrice
					};

					totalAmount += detail.SubTotal;
					await _detailRepo.AddAsync(detail);
				}
			
				newPurchase.TotalAmount = totalAmount;
				await _purchaseRepo.UpdateAsync(newPurchase);
				await _purchaseRepo.AddAsync(newPurchase);
			

				return Ok(new { message = "Compra creada con éxito", Data = newPurchase });
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Ocurrió un error inesperado al crear la compra: {ex.Message}");
			}
		}

		[HttpGet("{id}", Name = "GetPurchaseWithDetails")]
		public async Task<IActionResult> GetPurchaseWithDetails(int id)
		{
			try
			{
				if (id <= 0)
				{
					return BadRequest("El ID no es válido.");
				}

				
				var purchase = await _purchaseRepo.GetByIdAsync(id);
				if (purchase == null)
				{
					return NotFound($"No se encontró una compra con el ID {id}.");
				}

				
				var details = await _detailRepo.GetByIdAsync(id);

				return Ok(new
				{
					Purchase = purchase,
					Details = details
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Ocurrió un error inesperado al obtener la compra: {ex.Message}");
			}
		}

		[HttpPut("{id}", Name = "UpdatePurchaseAndDetails")]
		public async Task<IActionResult> UpdatePurchaseAndDetails(int id, UpdatePurchaseDTO purchaseDto)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				var purchase = await _purchaseRepo.GetByIdAsync(id);
				if (purchase == null)
				{
					return NotFound($"No se encontró una compra con el ID {id}.");
				}

				purchase.PurchaseDate = purchaseDto.PurchaseDate;
				purchase.SupplierID = purchaseDto.SupplierId;
				purchase.UserId = purchaseDto.UserId;

				decimal totalAmount = 0;

				
				foreach (var detailDto in purchaseDto.PurchaseDetails)
				{
					var detail = await _detailRepo.GetByIdAsync(detailDto.DetailID);
					if (detail != null)
					{
						
						detail.Quantity = detailDto.Quantity;
						detail.UnitPrice = detailDto.UnitPrice;
						detail.SubTotal = detailDto.Quantity * detailDto.UnitPrice;
						totalAmount += detail.SubTotal;

						
						await _detailRepo.UpdateAsync(detail);
					}
				}

				purchase.TotalAmount = totalAmount;

				
				await _purchaseRepo.UpdateAsync(purchase);

				return NoContent(); 
			}
			catch (Exception ex)
			{
				
				return StatusCode(500, $"Error interno del servidor: {ex.Message} detalle: {ex.InnerException?.Message}");
			}
		}


		[HttpDelete("{id}", Name = "DeletePurchaseWithDetails")]
		public async Task<IActionResult> DeletePurchaseWithDetails(int id)
		{
			try
			{
				var purchase = await _purchaseRepo.GetByIdAsync(id);
				if (purchase == null)
				{
					return NotFound($"No se encontró una compra con el ID {id}.");
				}

				var details = await _detailRepo.GetByPurchaseIdAsync(id);
				if (details.Any())
				{
					foreach (var detail in details)
					{
						await _detailRepo.DeleteAsync(detail.Id);
					}
				}
				await _purchaseRepo.DeleteAsync(id);

				return Ok(new { message = "Compra y sus detalles eliminados con éxito." });
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Ocurrió un error inesperado al eliminar la compra: {ex.Message}");
			}
		}

		[HttpDelete("details/{detailId}", Name = "DeletePurchaseDetail")]
		public async Task<IActionResult> DeletePurchaseDetail(int detailId)
		{
			try
			{
				var detail = await _detailRepo.GetByIdAsync(detailId);
				if (detail == null)
				{
					return NotFound($"No se encontró un detalle con el ID {detailId}.");
				}

				
				await _detailRepo.DeleteAsync(detailId);


				var sub = new PurchaseDetail();
				var purchase = await _purchaseRepo.GetByIdAsync(detail.Id);
				if (purchase != null)
				{
					
					purchase.TotalAmount -= sub.SubTotal;
					await _purchaseRepo.UpdateAsync(purchase);
				}

				return Ok(new { message = "Detalle de la compra eliminado con éxito." });
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Ocurrió un error inesperado al eliminar el detalle: {ex.Message}");
			}
		}


	}
}

