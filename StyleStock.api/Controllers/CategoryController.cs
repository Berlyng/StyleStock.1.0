using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StyleStock.domain;
using StyleStock.domain.DTOS;
using StyleStock.domain.Entities;
using StyleStock.domain.Repository;
using StyleStock.Infrastructure.Repositories;

namespace StyleStock.api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
	public class CategoryController : ControllerBase
	{
        private readonly ICategoryRepository _repo;

        public CategoryController(ICategoryRepository repo)
		{
            _repo = repo;
        }
		[HttpGet(Name = "GetCategory")]
		public async Task<IActionResult> GetCategory()
		{
			try
			{
				var categories = await _repo.GetAllAsync();

				if (categories == null)
				{
					return NotFound("Categori no encontrada");
				}

				return Ok(categories);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Ocurrió un error inesperado al obtener las categorías por {ex.Message}");



            }
		}

		[HttpPost(Name = "CreateCategory")]
		public async Task<IActionResult> CreateCategory(CreateCategoryDTO categoryDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var newCategory = new Category
				{
					Name = categoryDTO.Name,
					Description = categoryDTO.Description,
				};

				await _repo.AddAsync(newCategory);


				return Ok(new { Message = "Categoría creada exitosamente.", Data = newCategory });
			}
			catch (Exception)
			{

				return StatusCode(500, "Ocurrió un error inesperado al obtener las categorías.");

			}

		}

		[HttpGet("{id}", Name ="GetCategoryById")]
		public async Task<IActionResult> GetCategoryById(int id)
		{
			if (id <= 0)
			{
				return BadRequest("el id no es valido");
			}

			var search = await _repo.GetByIdAsync(id);
			return Ok(search);
		}

        [HttpPut("{CategoryId}", Name = "UpdateCategory" )]
		public async Task<IActionResult> UpdateCategory(int CategoryId, UpdateCategoryDTO category)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var updateCategory = _repo.GetByIdAsync(CategoryId).Result;

				if (updateCategory == null)
				{
					return NotFound();
				}

				updateCategory.Name = category.Name;
				updateCategory.Description = category.Description;

				await _repo.UpdateAsync(updateCategory);

				return Ok(updateCategory);
			}
			catch (Exception)
			{

				return StatusCode(500, "Ocurrió un error en la base de datos al intentar actualizar la categoría.");
			}

		}

		[HttpDelete("{id}",Name ="DeleteCategory")]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			try
			{
				var category =  await _repo.GetByIdAsync(id);
				if (category == null)
				{
					return NotFound();
				}

				await _repo.DeleteAsync(id);

				return Ok(new { Message = "Categoría eliminada exitosamente." });
			}
			catch (Exception)
			{

				return StatusCode(500, "Ocurrió un error en la base de datos al intentar eliminar la categoría.");
			}
		}
	}
}
