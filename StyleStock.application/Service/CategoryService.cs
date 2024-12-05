
using StyleStock.application.Interface;
using StyleStock.common.DTOS;
using StyleStock.domain.Entities;
using StyleStock.infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.application.Service
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository _categoryRepository;

		public CategoryService(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
		{
			try
			{
				var categories = await _categoryRepository.GetAllAsync();

				if (categories == null || !categories.Any())
				{
					throw new Exception("No se encontraron categorías.");
				}

				return categories.Select(c => new CategoryDTO
				{
					CategoryId = c.Id,
					Name = c.Name,
					Description = c.Description,
				});
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al obtener las categorías: {ex.Message}");
			}
		}


		public async Task<CategoryDTO> GetCategoryByIdAsync(int id)
		{
			try
			{
				var category = await _categoryRepository.GetByIdAsync(id);
				if (category == null)
				{
					throw new Exception($"Categoría con ID {id} no encontrada.");
				}

				return new CategoryDTO
				{
					CategoryId = category.Id,
					Name = category.Name,
					Description= category.Description,
				};
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al obtener las categorías: {ex.Message}");
			}
		}

		public async Task AddCategoryAsync(CreateCategoryDTO categoryDto)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(categoryDto.Name))
				{
					throw new Exception("El nombre de la categoría es obligatorio.");
				}

				var category = new Category
				{
					Name = categoryDto.Name,
					Description = categoryDto.Description
					
				};

				await _categoryRepository.AddAsync(category);
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al crear las categorías: {ex.Message}");
			}
		}

		public async Task UpdateCategoryAsync(int id, UpdateCategoryDTO categoryDto)
		{
			try
			{
				var category = await _categoryRepository.GetByIdAsync(id);
				if (category == null)
				{
					throw new Exception($"Categoría con ID {id} no encontrada.");
				}

				category.Name = categoryDto.Name;
				category.Description = categoryDto.Description;
				await _categoryRepository.UpdateAsync(category);
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al actualizar las categorías: {ex.Message}");
			}
		}

		public async Task DeleteCategoryAsync(int id)
		{
			try
			{
				await _categoryRepository.DeleteAsync(id);
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al eliminar las categorías: {ex.Message}");
			}
		}
	}
}
