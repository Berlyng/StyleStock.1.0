using StyleStock.application.Interface;
using StyleStock.common.DTOS;
using StyleStock.domain.Entities;
using StyleStock.infrastructure.Interface;
using StyleStock.infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PurchaseService : IPurchaseService
{
	private readonly IPurchaseRepository _purchaseRepository;

	public PurchaseService(IPurchaseRepository purchaseRepository)
	{
		_purchaseRepository = purchaseRepository;
	}

	public async Task AddPurchaseAsync(CreatePurchaseDTO purchaseDto)
	{
		try
		{
			// Validación de los datos de entrada
			if (purchaseDto.PurchaseDetails == null || !purchaseDto.PurchaseDetails.Any())
				throw new ArgumentException("Debe incluir detalles para la compra.");

			// Mapea los detalles de la compra y calcula el SubTotal para cada uno
			var purchaseDetails = purchaseDto.PurchaseDetails.Select(d => new PurchaseDetail
			{
				ProductId = d.ProductId,
				Quantity = d.Quantity,
				UnitPrice = d.UnitPrice, // Si tienes un precio por unidad
				SubTotal = d.Quantity * d.UnitPrice // Cálculo del SubTotal
			}).ToList();


			// Crea la entidad de compra
			var purchase = new Purchase
			{
				PurchaseDate = purchaseDto.PurchaseDate,
				UserId = purchaseDto.UserId,
				SupplierId = purchaseDto.SupplierId,
				TotalAmount = Math.Round(purchaseDto.TotalAmount, 2) // Usa el TotalAmount calculado en el DTO
			};

			// Asigna los detalles a la compra
			purchase.PurchaseDetails = purchaseDetails;

			// Agrega la compra al repositorio
			await _purchaseRepository.AddAsync(purchase);
		}
		catch (Exception ex)
		{
			throw new Exception($"Ocurrió un error al crear la compra: {ex.Message}", ex);
		}
	}

	public async Task DeletePurchaseAsync(int id)
	{
		try
		{
			await _purchaseRepository.DeleteAsync(id);
		}
		catch (Exception ex)
		{

			throw new Exception($"Ocurrió un error al eliminar las categorías: {ex.Message}");
		}
	}

	public async Task<IEnumerable<PurchaseDTO>> GetAllPurchaseAsync()
	{
		try
		{
			var purchase = await _purchaseRepository.GetAllAsync();
			if (purchase == null || !purchase.Any())
			{
				throw new Exception("No se encontraron los productos.");
			}

			return purchase.Select(p => new PurchaseDTO
			{
				PurchaseId = p.Id,
				PurchaseDate = p.PurchaseDate,
				UserId = p.UserId,
				SupplierId = p.SupplierId,
				TotalAmount = p.TotalAmount

			});

		}
		catch (Exception ex)
		{

			throw new Exception($"Ocurrió un error al obtener los pedidos: {ex.Message}");
		}

	}

	public async Task<PurchaseDTO> GetPurchaseByIdAsync(int id)
	{
		try
		{
			var purchase = await _purchaseRepository.GetByIdAsync(id);
			if (purchase == null)
			{
				throw new Exception($"producto con ID {id} no encontrada.");
			}

			return new PurchaseDTO
			{
				PurchaseId = purchase.Id,
				PurchaseDate = DateTime.Now,
				UserId = purchase.UserId,
				SupplierId = purchase.SupplierId,
				TotalAmount = purchase.TotalAmount,
				
			};
		}
		catch (Exception ex)
		{

			throw new Exception($"Ocurrió un error al obtener el Pedido: {ex.Message}");
		}
	}

	public async Task UpdatePurchaseAsync(int purchaseId, UpdatePurchaseDTO purchaseDto)
	{
		try
		{
			// Validación de los datos de entrada
			if (purchaseDto.PurchaseDetails == null || !purchaseDto.PurchaseDetails.Any())
				throw new ArgumentException("Debe incluir detalles para la compra.");

			// Obtén la compra existente por ID
			var purchase = await _purchaseRepository.GetByIdAsync(purchaseId);
			if (purchase == null)
				throw new ArgumentException("La compra no existe.");

			// Mapea los detalles de la compra y calcula el SubTotal para cada uno
			var purchaseDetails = purchaseDto.PurchaseDetails.Select(d => new PurchaseDetail
			{
				ProductId = d.ProductId,
				Quantity = d.Quantity,
				UnitPrice = d.UnitPrice, // Si tienes un precio por unidad
				SubTotal = d.Quantity * d.UnitPrice // Cálculo del SubTotal
			}).ToList();

			// Actualiza la entidad de compra
			purchase.PurchaseDate = purchaseDto.PurchaseDate;
			purchase.UserId = purchaseDto.UserId;
			purchase.SupplierId = purchaseDto.SupplierId;
			purchase.TotalAmount = Math.Round(purchaseDto.TotalAmount, 2); // Usa el TotalAmount calculado en el DTO

			// Actualiza los detalles de la compra
			purchase.PurchaseDetails = purchaseDetails;

			// Actualiza la compra en el repositorio
			await _purchaseRepository.UpdateAsync(purchase);
		}
		catch (Exception ex)
		{
			throw new Exception($"Ocurrió un error al actualizar la compra: {ex.Message}", ex);
		}
	}

}


