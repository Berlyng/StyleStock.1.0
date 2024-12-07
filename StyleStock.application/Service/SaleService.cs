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

namespace StyleStock.application.Service
{
	public class SaleService : ISaleService
	{
		private readonly ISaleRepository _saleRepository;

		public SaleService(ISaleRepository saleRepository)
		{
			_saleRepository = saleRepository;
		}

		public async Task AddSaleAsync(CreateSaleDTO saleDto)
		{
			try
			{
				var sale = new Sale
				{
					SaleDate = DateTime.Now,
					CustomerId = saleDto.CustomerId,
					UserId = saleDto.UserId,
			
				};

				await _saleRepository.AddAsync(sale);
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al crear las ventas: {ex.Message}");
			}
		}

		public async Task DeleteSaleAsync(int id)
		{
			try
			{
				await _saleRepository.DeleteAsync(id);
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al eliminar las categorías: {ex.Message}");
			}
		}

		public async Task<IEnumerable<SaleDTO>> GetAllSalesAsync()
		{
			try
			{
				var sales = await _saleRepository.GetAllAsync();
				if (sales == null || !sales.Any())
				{
					throw new Exception("No se encontraron las ventas.");
				}

				return sales.Select(p => new SaleDTO
				{
					SaleDate = p.SaleDate,
					CustomerId = p.CustomerId,
					UserId = p.UserId,
					TotalAmount = p.SalesDetails.Sum(d => d.SubTotal) * 0.18m

				});

			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al obtener las ventas: {ex.Message}");
			}
		}

		public async Task<SaleDTO> GetSaleByIdAsync(int id)
		{
			try
			{
				var purchase = await _saleRepository.GetByIdAsync(id);
				if (purchase == null)
				{
					throw new Exception($"venta con ID {id} no encontrada.");
				}

				return new SaleDTO
				{
					SaleDate = purchase.SaleDate,
					CustomerId = purchase.CustomerId,
					UserId = purchase.UserId,
					TotalAmount = purchase.TotalAmount,

				};
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al obtener la venta: {ex.Message}");
			}
		}

		public async Task UpdateSaleAsync(int id, UpdateSaleDTO saleDto)
		{

			try
			{
				var sale = await _saleRepository.GetByIdAsync(id);
				if (sale == null)
				{
					throw new Exception($"Producto con ID {id} no encontrada.");
				}
				sale.SaleDate = saleDto.SaleDate;
				sale.CustomerId = saleDto.CustomerId;
				sale.UserId = saleDto.UserId;

				await _saleRepository.UpdateAsync(sale);
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al actualizar las ventas: {ex.Message}");
			}
		}
	}
}
