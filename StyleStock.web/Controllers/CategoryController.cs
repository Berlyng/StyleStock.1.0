using Microsoft.AspNetCore.Mvc;
using StyleStock.domain.DTOS;

namespace StyleStock.web.Controllers
{
	public class CategoryController : Controller
	{
        private readonly HttpClient _client;

        public CategoryController(HttpClient client)
		{
		
            _client = client;
        }
		public async Task<IActionResult> Index()
		{
			var categories = await _client.GetFromJsonAsync<IEnumerable<CategoryDTO>> ("http://localhost:5068/api/Category/GetCategory");
			if(categories == null)
			{
				ViewBag.ErrorMessage = "No se pudo encontrar las categorias";
				return View();
			}
			return View(categories);
		}

		[HttpGet]
		public IActionResult Create()
		{

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateCategoryDTO newCategory)
		{

			if (!ModelState.IsValid)
			{
                ViewBag.ErrorMessage = "Por favor complete todos los campos requeridos";
                return View(newCategory);
			}
			try
			{
			
				var response = await _client.PostAsJsonAsync("http://localhost:5068/api/Category/CreateCategory", newCategory);
				if (response.IsSuccessStatusCode)
				{
					return RedirectToAction("Index");
				}
				else 
				{
                    ViewBag.ErrorMessage = "Hubo un error al crear la categoria.";
                    return View(newCategory);
                }
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
			try
			{

				var response = await _client.GetAsync($"http://localhost:5068/api/Category/GetCategoryById/{id}");
				if (response == null)
				{
					return NotFound();
				}
				if (response.IsSuccessStatusCode)
				{
					var category = await response.Content.ReadFromJsonAsync<UpdateCategoryDTO>();
					if (category == null)
					{
						ViewBag.ErrorMessage = "No se encontraron datos de la Categotira.";
						return RedirectToAction("Index");
					}

					return View(category);
				}
				else
				{
					ViewBag.ErrorMessage = "Hubo un error al obtener la información.";
					return RedirectToAction("Index");
				}
			}
			catch (Exception )
			{

                ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar actualizar la categoría.");
                return View();
            }
			
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
				var response = await _client.PutAsJsonAsync($"http://localhost:5068/api/Category/UpdateCategory/{category.Id}",category);

                if (response == null || !response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "No se pudo actualizar la categoría.");
                    return View(category);
                }

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
			if (id <= 0)
			{
				TempData["ErrorMessage"] = "El ID proporcionado no es válido.";
				return RedirectToAction("Index");
			}

			try
			{
				var response = await _client.DeleteAsync($"http://localhost:5068/api/Category/DeleteCategory/{id}");

				if (response.IsSuccessStatusCode)
				{
					TempData["SuccessMessage"] = "La categoría se eliminó correctamente.";
				}
				else
				{
					var errorContent = await response.Content.ReadAsStringAsync();
					TempData["ErrorMessage"] = $"Hubo un error al eliminar la categoría: {errorContent}";
				}

				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = $"Error inesperado: {ex.Message}";
				return RedirectToAction("Index");
			}
		}
	}
}
