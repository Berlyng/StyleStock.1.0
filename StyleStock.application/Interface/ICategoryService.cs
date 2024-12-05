using StyleStock.common.DTOS;
using StyleStock.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.application.Interface
{
	public interface ICategoryService
	{

		Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
		Task<CategoryDTO> GetCategoryByIdAsync(int id);
		Task AddCategoryAsync(CreateCategoryDTO categoryDto);
		Task UpdateCategoryAsync(int id, UpdateCategoryDTO categoryDto);
		Task DeleteCategoryAsync(int id);


	}
}
