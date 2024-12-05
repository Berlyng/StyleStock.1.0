using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StyleStock.application.Interface;
using StyleStock.common.DTOS;

namespace StyleStock.api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		[HttpGet(Name = "GetCategory")]
		public async Task<IActionResult> GetAllCategories()
		{
			try
			{
				var categories = await _categoryService.GetAllCategoriesAsync();
				return Ok(categories);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{id}", Name = "GetCategoryById")]
		public async Task<IActionResult> GetCategoryById(int id)
		{
			try
			{
				var category = await _categoryService.GetCategoryByIdAsync(id);
				return Ok(category);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpPost(Name = "AddCategory")]
		public async Task<IActionResult> AddCategory(CreateCategoryDTO categoryDto)
		{
			try
			{
				await _categoryService.AddCategoryAsync(categoryDto);
				return Ok(new { Message = "Categoría creada exitosamente", Data = categoryDto });
			}
			catch (Exception ex)
			{
				return BadRequest(new { Error = ex.Message });
			}
		}

		[HttpPut("{id}", Name = "UpdateCategory")]
		public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDTO categoryDto)
		{
			try
			{
				await _categoryService.UpdateCategoryAsync(id, categoryDto);
				return NoContent();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{id}", Name ="DeleteCategory")]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			try
			{
				await _categoryService.DeleteCategoryAsync(id);
				return NoContent();
			}
			catch (Exception ex)
			{

				return NotFound(ex.Message);
			}
		}
	}
}
