using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StyleStock.common.DTOS;

namespace StyleStock.web.Controllers
{
	public class ProductsController : Controller
	{
		private readonly HttpClient _client;

		public ProductsController(HttpClient client)
		{

			_client = client;
		}

		public async Task<IActionResult> Index()
		{
			try
			{
				var products = await _client.GetFromJsonAsync<IEnumerable<ProductDTO>>("https://localhost:7237/api/Product/GetAllProduct");
				if (products == null)
				{
					ViewBag.ErrorMessage = "No se pudo encontrar los productos.";
					return View();
				}

				var categories = await _client.GetFromJsonAsync<IEnumerable<CategoryDTO>>("https://localhost:7237/api/Category/GetAllCategories");
				if (categories == null)
				{
					ViewBag.ErrorMessage = "No se pudo encontrar las categorías.";
					return View(products);
				}

				var productsWithCategoryNames = products.Select(product =>
				{
					var category = categories.FirstOrDefault(c => c.CategoryId == product.CategoryId);
					product.CategoryName = category?.Name ?? "Categoría no encontrada";
					return product;
				});

				return View(productsWithCategoryNames);
			}
			catch (Exception ex)
			{
				ViewBag.ErrorMessage = $"Ocurrió un error: {ex.Message}";
				return View();
			}
		}


		[HttpGet]
		public async Task<IActionResult> Create()
		{
			try
			{
				var response = await _client.GetAsync("https://localhost:7237/api/Category/GetAllCategories");

				if (response.IsSuccessStatusCode)
				{
					var categories = await response.Content.ReadFromJsonAsync<List<CategoryDTO>>();


					ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
				}
				else
				{
					ViewBag.ErrorMessage = "No se pudieron obtener las categorías.";
				}
			}
			catch (Exception ex)
			{
				ViewBag.ErrorMessage = "Error al obtener las categorías: " + ex.Message;
			}

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateProductDTO newProduct)
		{
			if (!ModelState.IsValid)
			{
				return View(newProduct);
			}
			try
			{

				var response = await _client.PostAsJsonAsync("https://localhost:7237/api/api/Product/AddProducts", newProduct);
				if (response.IsSuccessStatusCode)
				{
					return RedirectToAction("Index");
				}
				else
				{
					ViewBag.ErrorMessage = "Hubo un error al crear la categoria.";
					return View(newProduct);
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
				return BadRequest("ID de producto no válido.");
			}

			try
			{
				
				var response = await _client.GetAsync($"https://localhost:7237/api/Product/GetProductById/{id}");
				if (response.IsSuccessStatusCode)
				{
					var product = await response.Content.ReadFromJsonAsync<UpdateProductDTO>();
					if (product == null)
					{
						ViewBag.ErrorMessage = "No se encontraron datos del producto.";
						return RedirectToAction("Index");
					}

					
					var categoryResponse = await _client.GetAsync("https://localhost:7237/api/Category/GetAllCategories");
					if (categoryResponse.IsSuccessStatusCode)
					{
						var categories = await categoryResponse.Content.ReadFromJsonAsync<List<CategoryDTO>>();
						ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
					}

					return View(product);
				}
				else
				{
					ViewBag.ErrorMessage = "Hubo un error al obtener los datos del producto.";
					return RedirectToAction("Index");
				}
			}
			catch (Exception ex)
			{
				ViewBag.ErrorMessage = "Error al obtener los datos del producto: " + ex.Message;
				return RedirectToAction("Index");
			}
		}

		[HttpPost]
		public async Task<IActionResult> Update(UpdateProductDTO updateProductDto)
		{
			if (!ModelState.IsValid)
			{
				try
				{
					var categoryResponse = await _client.GetAsync("https://localhost:7237/api/Category/GetAllCategories");
					if (categoryResponse.IsSuccessStatusCode)
					{
						var categories = await categoryResponse.Content.ReadFromJsonAsync<List<CategoryDTO>>();
						ViewBag.Categories = new SelectList(categories, "Id", "Name");
					}
				}
				catch (Exception ex)
				{
					ViewBag.ErrorMessage = "Error al cargar las categorías: " + ex.Message;
				}

				return View(updateProductDto);
			}

			try
			{
				
				var response = await _client.PutAsJsonAsync($"https://localhost:7237/api/Product/UpdateProduct/{updateProductDto.ProductId}", updateProductDto);

				if (response.IsSuccessStatusCode)
				{
					return RedirectToAction("Index");
				}
				else
				{
					ViewBag.ErrorMessage = "Hubo un error al actualizar el producto.";
					return View(updateProductDto);
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar actualizar el producto: " + ex.Message);
				return View(updateProductDto);
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
				var response = await _client.DeleteAsync($"https://localhost:7237/api/Product/DeleteProduct/{id}");

				if (response.IsSuccessStatusCode)
				{
					TempData["SuccessMessage"] = "El producto se eliminó correctamente.";
				}
				else
				{
					var errorContent = await response.Content.ReadAsStringAsync();
					TempData["ErrorMessage"] = $"Hubo un error al eliminar el producto: {errorContent}";
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
