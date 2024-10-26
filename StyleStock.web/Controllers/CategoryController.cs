using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Identity.Client;
using StyleStock.domain;
using StyleStock.domain.DTOS;
using StyleStock.domain.Entities;

namespace StyleStock.web.Controllers
{
	public class CategoryController : Controller
	{
		private readonly StyleStockContext _context;

		public CategoryController(StyleStockContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			var categories = _context.Categories
				.Select(c => new CategoryDTO
				{
					CategoryId = c.CategoryId,
					Name = c.Name,
					Description = c.Description,
				}).ToList();
			return View(categories);
		}

		[HttpGet]
		public IActionResult Create()
		{

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateCategoryDTO categoryDTO)
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

				_context.Categories.Add(newCategory);
				await _context.SaveChangesAsync();


				return RedirectToAction(nameof(Index));
			}
			catch (Exception)
			{

				ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar crear la categoría.");
				return View();
			}

		}
		[HttpGet]
		public async Task<IActionResult> Update(int id)
		{
			if (id <= 0)
			{
				return BadRequest("ID categoria no valido");
			}

			var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
			if (category == null)
			{
				return NotFound();
			}

			var categoryDTO = new UpdateCategoryDTO
			{
				CategoryId = category.CategoryId,
				Name = category.Name,
				Description = category.Description,
			};

			return View(categoryDTO);

			
		}

		[HttpPost]
		public async Task<IActionResult> Update(UpdateCategoryDTO category)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var updateCategory = await _context.Categories.FindAsync(category.CategoryId);

				if (updateCategory == null)
				{
					return NotFound();
				}

				updateCategory.Name = category.Name;
				updateCategory.Description = category.Description;

				_context.Categories.Update(updateCategory);
				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}
			catch (Exception)
			{

				ModelState.AddModelError(string.Empty, "Ocurrió un error en la base de datos al intentar actualizar la categoría.");
				return View(category); 
			}	

		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
				if (category == null)
				{
					return NotFound();
				}

				_context.Categories.Remove(category);
				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}
			catch (Exception)
			{

				ModelState.AddModelError(string.Empty, "Ocurrió un error en la base de datos al intentar Eliminar la categoría.");
				return View(); 
			}
		}
	}
}
