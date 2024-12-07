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
	public class SalesDetailService : ISalesDetailService
	{
		private readonly ISalesDetailRepository _salesDetailRepository;
		private readonly IProductRepository _productRepository;
		private readonly ISaleRepository _saleRepository;

		public SalesDetailService(ISalesDetailRepository salesDetailRepository, IProductRepository productRepository, ISaleRepository saleRepository)
		{
			_salesDetailRepository = salesDetailRepository;
			_productRepository = productRepository;
			_saleRepository = saleRepository;
		}
		public async Task AddSaleAsync(CreateSalesDetailDTO detailDto)
		{
			try
			{

				var sale = await _saleRepository.GetByIdAsync(detailDto.SaleId);
				if (sale == null)
				{
					throw new Exception("El ID de la venta no existe.");
				}


				var product = await _productRepository.GetByIdAsync(detailDto.ProductId);
				if (product == null)
				{
					throw new Exception("El ID del producto no existe.");
				}


				var salesDetail = new SalesDetail
				{
					SaleId = detailDto.SaleId,
					ProductId = detailDto.ProductId,
					UnitPrice = detailDto.UnitPrice,
					Quantity = detailDto.Quantity,
				};

				await _salesDetailRepository.AddAsync(salesDetail);
			}
			catch (Exception ex)
			{
				throw new Exception($"Ocurrió un error al crear el detalle de compra: {ex.Message}");
			}
		}

		public async Task DeleteSaleAsync(int id)
		{
			try
			{
				await _salesDetailRepository.DeleteAsync(id);
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al eliminar las categorías: {ex.Message}");
			}
		}

		public async Task<IEnumerable<SalesDetailDTO>> GetAllSalesAsync()
		{
			try
			{
				var details = await _salesDetailRepository.GetAllAsync();
				if (details == null || !details.Any())
				{
					throw new Exception("No se encontraron las ventas.");
				}

				return details.Select(p => new SalesDetailDTO
				{
					DetailId = p.Id,
					SaleId=p.SaleId,
					ProductId = p.ProductId,
					UnitPrice = p.UnitPrice,
					Quantity = p.Quantity,
					SubTotal = p.Quantity * p.UnitPrice
				}).ToList();

			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al obtener las ventas: {ex.Message}");
			}
		}

		public async Task<SalesDetailDTO> GetSaleByIdAsync(int id)
		{
			try
			{
				var detail = await _salesDetailRepository.GetByIdAsync(id);
				if (detail == null)
				{
					throw new Exception($"ventas con ID {id} no encontrada.");
				}

				return new SalesDetailDTO
				{
					DetailId = detail.Id,
					SaleId = detail.SaleId,
					ProductId = detail.ProductId,
					UnitPrice = detail.UnitPrice,
					Quantity = detail.Quantity,

				};
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al obtener la venta: {ex.Message}");
			}
		}

		public async Task UpdateSaleAsync(int id, UpdateSalesDetailDTO detailDto)
		{
			try
			{
				var detail = await _salesDetailRepository.GetByIdAsync(id);
				if (detail == null)
				{
					throw new Exception($"Detalle de con ID {id} no encontrada.");
				}

				detail.SaleId = detailDto.SaleId;
				detail.ProductId = detailDto.ProductId;
				detail.UnitPrice = detailDto.UnitPrice;
				detail.Quantity = detailDto.Quantity;

				await _salesDetailRepository.UpdateAsync(detail);
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al actualizar los productos: {ex.Message}");
			}
		}
	}
}
