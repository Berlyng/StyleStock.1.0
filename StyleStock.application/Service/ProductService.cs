using Microsoft.Identity.Client;
using StyleStock.application.Interface;
using StyleStock.common.DTOS;
using StyleStock.domain.Entities;
using StyleStock.infrastructure.Interface;
using StyleStock.infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.application.Service
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;

		public ProductService(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task AddProductAsycn(CreateProductDTO productDto)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(productDto.Name))
				{
					throw new Exception("El nombre del producto es obligatorio.");
				}

				var product = new Product
				{
					Name = productDto.Name,
					Description = productDto.Description,
					Price = productDto.Price,
					Size = productDto.Size,
					Color = productDto.Color,
					CategoryId = productDto.CategoryId,
					StockQuantity = productDto.StockQuantity,
					EntryDate = productDto.EntryDate,
				};

				await _productRepository.AddAsync(product);
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al crear los productos: {ex.Message}");
			}
		}

		public async Task DeleteProductAsync(int id)
		{
			try
			{
				await _productRepository.DeleteAsync(id);
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al eliminar las categorías: {ex.Message}");
			}
		}

		public async Task<IEnumerable<ProductDTO>> GetAllProductAsync()
		{
			try
			{
				var products = await _productRepository.GetAllAsync();
				if(products == null || !products.Any())
				{
					throw new Exception("No se encontraron los productos.");
				}

				return products.Select(p => new ProductDTO
				{
					ProductId = p.Id,
					Name = p.Name,
					Description = p.Description,
					Price = p.Price,
					Size = p.Size,
					Color = p.Color,
					CategoryId = p.CategoryId,
					StockQuantity = p.StockQuantity,
					EntryDate = p.EntryDate,
				});

			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al obtener los productos: {ex.Message}");
			}
		
		}

		public async Task<ProductDTO> GetProductByIdAsync(int id)
		{
			try
			{
				var product = await _productRepository.GetByIdAsync(id);
				if (product == null)
				{
					throw new Exception($"producto con ID {id} no encontrada.");
				}

				return new ProductDTO
				{
					ProductId = product.Id,
					Name = product.Name,
					Description = product.Description,
					Size = product.Size,
					Color = product.Color,
					CategoryId = product.CategoryId,
					StockQuantity = product.StockQuantity,
					EntryDate = product.EntryDate,
				};
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al obtener el productos: {ex.Message}");
			}
		}

		public async Task UpdateProductAsync(int id, UpdateProductDTO productDto)
		{

			try
			{
				var product = await _productRepository.GetByIdAsync(id);
				if (product == null)
				{
					throw new Exception($"Producto con ID {id} no encontrada.");
				}

				product.Name = productDto.Name;
				product.Description = productDto.Description;
				product.Price = productDto.Price;
				product.Size = productDto.Size;
				product.Color = productDto.Color;
				product.CategoryId = productDto.CategoryId;
				product.StockQuantity = productDto.StockQuantity;

				await _productRepository.UpdateAsync(product);
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al actualizar los productos: {ex.Message}");
			}
		}
	}
}
