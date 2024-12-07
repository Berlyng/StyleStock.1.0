using StyleStock.application.Interface;
using StyleStock.common.DTOS;
using StyleStock.domain.Entities;
using StyleStock.infrastructure.Core;
using StyleStock.infrastructure.Interface;
using StyleStock.infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.application.Service
{
	public class PurchaseDetailService : IPurchaseDetailService
	{
		private readonly IPurchaseDetailRepository _detailRepository;
		public IProductRepository _productRepository { get; }
		public IPurchaseRepository _purchaseRepository { get; }

		public PurchaseDetailService(IPurchaseDetailRepository detailRepository, IProductRepository productRepository, IPurchaseRepository purchaseRepository)
		{
			_detailRepository = detailRepository;
			_productRepository = productRepository;
			_purchaseRepository = purchaseRepository;
		}



		public async Task AddPurchaseDetailAsycn(CreatePurchaseDetailDTO detailDto)
		{
			try
			{
				
			
				var purchase =  _purchaseRepository.GetByIdAsync(detailDto.PurchaseId).Result;
				if (purchase == null)
				{
					throw new Exception("El ID del pedido no existe.");
				}


				var product = _productRepository.GetByIdAsync(detailDto.ProductId).Result;
				if (product == null)
				{
					throw new Exception("El ID del producto no existe.");
				}

				
				var purchaseDetail = new PurchaseDetail
				{
					PurchaseId = detailDto.PurchaseId,
					ProductId = detailDto.ProductId,
					UnitPrice = detailDto.UnitPrice,
					Quantity = detailDto.Quantity,
				};

				await _detailRepository.AddAsync(purchaseDetail);
			}
			catch (Exception ex)
			{
				throw new Exception($"Ocurrió un error al crear el detalle de compra: {ex.Message}");
			}
		}

		public async Task DeletePurchaseDetailAsync(int id)
		{
			try
			{
				await _detailRepository.DeleteAsync(id);
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al eliminar las categorías: {ex.Message}");
			}
		}

		public async Task<IEnumerable<PurchaseDetailDTO>> GetAllPurchaseDetailAsync()
		{
			try
			{
				var details = await _detailRepository.GetAllAsync();
				if (details == null || !details.Any())
				{
					throw new Exception("No se encontraron los pedidos.");
				}

				return details.Select(p => new PurchaseDetailDTO
				{
					DetailId = p.Id,
					PurchaseId = p.PurchaseId,
					ProductId = p.ProductId,
					UnitPrice = p.UnitPrice,
					Quantity = p.Quantity,
					SubTotal = p.Quantity * p.UnitPrice
				}).ToList();

			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al obtener los pedidos: {ex.Message}");
			}

		}

		public async Task<PurchaseDetailDTO> GetPurchaseDetailByIdAsync(int id)
		{
			try
			{
				var purchase = await _detailRepository.GetByIdAsync(id);
				if (purchase == null)
				{
					throw new Exception($"producto con ID {id} no encontrada.");
				}

				return new PurchaseDetailDTO
				{
					DetailId= purchase.Id,
					PurchaseId= purchase.PurchaseId,
					ProductId= purchase.ProductId,
					UnitPrice = purchase.UnitPrice,
					Quantity = purchase.Quantity,

				};
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al obtener el productos: {ex.Message}");
			}
		}

		public async Task UpdatePurchaseDetailAsync(int id, UpdatePurchaseDetailDTO productDto)
		{

			try
			{
				var detail = await _detailRepository.GetByIdAsync(id);
				if (detail == null)
				{
					throw new Exception($"Detalle de con ID {id} no encontrada.");
				}

				detail.PurchaseId = productDto.PurchaseId;
				detail.ProductId = productDto.ProductId;
				detail.UnitPrice = productDto.UnitPrice;
				detail.Quantity = productDto.Quantity;

				await _detailRepository.UpdateAsync(detail);
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al actualizar los productos: {ex.Message}");
			}
		}

	}

}
