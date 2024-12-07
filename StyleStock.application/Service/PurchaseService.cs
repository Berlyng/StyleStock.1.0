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


	public async Task AddPurchaseAsycn(CreatePurchaseDTO purchaseDto)
	{
		try
		{
			var purchase = new Purchase
			{
				PurchaseDate = purchaseDto.PurchaseDate,
				UserId = purchaseDto.UserId,
				SupplierId = purchaseDto.SupplierId,
			};

			await _purchaseRepository.AddAsync(purchase);
		}
		catch (Exception ex)
		{

			throw new Exception($"Ocurrió un error al crear los purchase: {ex.Message}");
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
				PurchaseDate = DateTime.Now,
				UserId = p.UserId,
				SupplierId = p.SupplierId,
				TotalAmount = p.PurchaseDetails.Sum(d => d.SubTotal) * 0.18m

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

	public async Task UpdatePurchaseAsync(int id, UpdatePurchaseDTO purchaseDto)
	{

		try
		{
			var purchase = await _purchaseRepository.GetByIdAsync(id);
			if (purchase == null)
			{
				throw new Exception($"Producto con ID {id} no encontrada.");
			}
			purchase.PurchaseDate = DateTime.Now;
			purchase.UserId = purchaseDto.UserId;
			purchase.SupplierId = purchaseDto.SupplierId;

			await _purchaseRepository.UpdateAsync(purchase);
		}
		catch (Exception ex)
		{

			throw new Exception($"Ocurrió un error al actualizar los pedidos: {ex.Message}");
		}
	}
}


