using Microsoft.AspNetCore.Mvc;
using StyleStock.common.DTOS;

namespace StyleStock.web.Controllers
{
	public class SupplierController : Controller
	{
		private readonly HttpClient _client;

		public SupplierController(HttpClient client)
		{
			_client = client;
		}
		public async Task<IActionResult> Index()
		{
			var suppliers = await _client.GetFromJsonAsync<IEnumerable<SupplierDTO>>("https://localhost:7237/api/Supplier/GetAllSupplier");
			if (suppliers == null)
			{
				ViewBag.ErrorMessage = "No se pudo encontrar los suplidores";
				return View();
			}
			return View(suppliers);
		}

		[HttpGet]
		public IActionResult Create()
		{

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateSupplierDTO newSupplier)
		{
			if (!ModelState.IsValid)
			{
				return View(newSupplier);
			}
			try
			{

				var response = await _client.PostAsJsonAsync("https://localhost:7237/api/Supplier/AddSupplier/AddSupplier", newSupplier);
				if (response.IsSuccessStatusCode)
				{
					return RedirectToAction("Index");
				}
				else
				{
					ViewBag.ErrorMessage = "Hubo un error al crear el suplidor.";
					return View(newSupplier);
				}
			}
			catch (Exception)
			{

				ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar crear el supplidor.");
				return View();
			}

		}

		[HttpGet]
		public async Task<IActionResult> Update(int id)
		{
			if (id <= 0)
			{
				return BadRequest("ID Suplidor no valido");
			}
			try
			{

				var response = await _client.GetAsync($"https://localhost:7237/api/Supplier/GetAllSupplierById/{id}");
				if (response == null)
				{
					return NotFound();
				}
				if (response.IsSuccessStatusCode)
				{
					var supplier = await response.Content.ReadFromJsonAsync<UpdateSupplierDTO>();
					if (supplier == null)
					{
						ViewBag.ErrorMessage = "No se encontraron datos de los suplidores.";
						return RedirectToAction("Index");
					}

					return View(supplier);
				}
				else
				{
					ViewBag.ErrorMessage = "Hubo un error al obtener la información.";
					return RedirectToAction("Index");
				}
			}
			catch (Exception)
			{

				ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar actualizar el supplidor.");
				return View();
			}

		}

		[HttpPost]
		public async Task<IActionResult> Update(UpdateSupplierDTO supplier)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var response = await _client.PutAsJsonAsync($"https://localhost:7237/api/Supplier/UpdateSupplier/{supplier.SupplierId}", supplier);

				if (response == null || !response.IsSuccessStatusCode)
				{
					ModelState.AddModelError(string.Empty, "No se pudo actualizar el suplidor.");
					return View(supplier);
				}

				return RedirectToAction(nameof(Index));

			}
			catch (Exception)
			{

				ModelState.AddModelError(string.Empty, "Ocurrió un error en la base de datos al intentar actualizar el suplidor.");
				return View(supplier);
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
				var response = await _client.DeleteAsync($"https://localhost:7237/api/Supplier/DeleteSupplier/{id}");

				if (response.IsSuccessStatusCode)
				{
					TempData["SuccessMessage"] = "La categoría se eliminó correctamente.";
				}
				else
				{
					var errorContent = await response.Content.ReadAsStringAsync();
					TempData["ErrorMessage"] = $"Hubo un error al eliminar el suplidor: {errorContent}";
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
