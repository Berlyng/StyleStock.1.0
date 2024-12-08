using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StyleStock.common.DTOS;

namespace StyleStock.web.Controllers
{
	public class PurchaseDetailController : Controller
	{
		private readonly HttpClient _client;

		public PurchaseDetailController(HttpClient client)
		{
			_client = client;
		}

		[HttpGet]
		public async Task<IActionResult> GetPurchaseDetails(int id)
		{
			if (id <= 0)
			{
				return BadRequest("ID de pedido no válido.");
			}

			try
			{
				// Obtener los datos de la compra
				var purchaseResponse = await _client.GetAsync($"https://localhost:7237/api/Purchase/GetPurchaseById/{id}");
				if (!purchaseResponse.IsSuccessStatusCode)
				{
					ViewBag.ErrorMessage = "Hubo un error al obtener los datos del pedido.";
					return RedirectToAction("Index");
				}

				var purchase = await purchaseResponse.Content.ReadFromJsonAsync<UpdatePurchaseDTO>();
				if (purchase == null)
				{
					ViewBag.ErrorMessage = "No se encontraron datos del pedido.";
					return RedirectToAction("Index");
				}

				// Obtener el nombre del suplidor
				var supplierResponse = await _client.GetAsync($"https://localhost:7237/api/Supplier/GetSupplierById/{purchase.SupplierId}");
				string supplierName = "Desconocido";
				if (supplierResponse.IsSuccessStatusCode)
				{
					var supplier = await supplierResponse.Content.ReadFromJsonAsync<SupplierDTO>();
					supplierName = supplier?.Name ?? "Desconocido";
				}

				// Obtener los detalles de la compra
				var detailsResponse = await _client.GetAsync($"https://localhost:7237/api/Purchase/GetPurchaseDetails/{id}");
				var details = new List<PurchaseDetailDTO>();
				if (detailsResponse.IsSuccessStatusCode)
				{
					details = await detailsResponse.Content.ReadFromJsonAsync<List<PurchaseDetailDTO>>() ?? new List<PurchaseDetailDTO>();
				}

				// Enviar datos a la vista a través de ViewData
				ViewData["SupplierName"] = supplierName;
				ViewData["PurchaseDate"] = purchase.PurchaseDate;
				ViewData["PurchaseId"] = purchase.PurchaseId;

				return View(details);
			}
			catch (Exception ex)
			{
				ViewBag.ErrorMessage = "Error al obtener los datos del pedido: " + ex.Message;
				return RedirectToAction("Index");
			}
		}

		[HttpGet]
		public async Task<IActionResult> CreatePurchaseDetail(int purchaseId)
		{
			// Asegurarse de que el PurchaseId esté disponible para los detalles de la compra
			if (TempData["PurchaseId"] == null)
			{
				TempData["ErrorMessage"] = "No se encontró un ID de compra válido.";
				return RedirectToAction("Create");
			}

			// Obtener la lista de productos para el formulario de detalles
			var productResponse = await _client.GetAsync("https://localhost:7237/api/Product/GetAllProduct");
			if (productResponse.IsSuccessStatusCode)
			{
				var products = await productResponse.Content.ReadFromJsonAsync<List<ProductDTO>>();
				ViewBag.Products = new SelectList(products, "ProductId", "Name");
			}

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreatePurchaseDetail(CreatePurchaseDTO purchaseDetails)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return View(purchaseDetails);
				}

				// Aquí puedes llamar al servicio para agregar los detalles de la compra

				TempData["DebugMessage"] = "Detalles de compra agregados exitosamente.";
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, "Ocurrió un error al agregar los detalles de la compra: " + ex.Message);
				return View(purchaseDetails);
			}
		}

		[HttpGet]
		public async Task<IActionResult> UpdatePurchaseDeail(int id)
		{
			if (id <= 0)
			{
				return BadRequest("ID de pedido no válido.");
			}

			try
			{
				// Obtener datos de la compra
				var response = await _client.GetAsync($"https://localhost:7237/api/Purchase/GetPurchaseById/{id}");
				if (!response.IsSuccessStatusCode)
				{
					ViewBag.ErrorMessage = "Hubo un error al obtener los datos del pedido.";
					return RedirectToAction("Index");
				}

				var purchase = await response.Content.ReadFromJsonAsync<UpdatePurchaseDTO>();
				if (purchase == null)
				{
					ViewBag.ErrorMessage = "No se encontraron datos del pedido.";
					return RedirectToAction("Index");
				}

				// Obtener suplidores para el dropdown
				var supplierResponse = await _client.GetAsync("https://localhost:7237/api/Supplier/GetAllSupplier");
				if (supplierResponse.IsSuccessStatusCode)
				{
					var suppliers = await supplierResponse.Content.ReadFromJsonAsync<List<SupplierDTO>>();
					ViewBag.Suppliers = new SelectList(suppliers, "SupplierId", "Name");
				}
				else
				{
					ViewBag.ErrorMessage = "No se pudieron obtener los suplidores.";
				}

				return View(purchase);
			}
			catch (Exception ex)
			{
				ViewBag.ErrorMessage = "Error al obtener los datos del pedido: " + ex.Message;
				return RedirectToAction("Index");
			}
		}

		[HttpPost]
		public async Task<IActionResult> UpdatePurchaseDeail(UpdatePurchaseDTO updatePurchase)
		{
			if (!ModelState.IsValid)
			{
				try
				{
					// Cargar nuevamente los suplidores si hay un error de validación
					var supplierResponse = await _client.GetAsync("https://localhost:7237/api/Supplier/GetAllSupplier");
					if (supplierResponse.IsSuccessStatusCode)
					{
						var suppliers = await supplierResponse.Content.ReadFromJsonAsync<List<SupplierDTO>>();
						ViewBag.Suppliers = new SelectList(suppliers, "SupplierId", "Name");
					}
				}
				catch (Exception ex)
				{
					ViewBag.ErrorMessage = "Error al cargar los suplidores: " + ex.Message;
				}

				return View(updatePurchase);
			}

			try
			{
				// Actualizar los datos de la compra
				var response = await _client.PutAsJsonAsync($"https://localhost:7237/api/Purchase/UpdatePurchase/{updatePurchase.PurchaseId}", updatePurchase);

				if (response.IsSuccessStatusCode)
				{
					return RedirectToAction("UpdateDetails", new { purchaseId = updatePurchase.PurchaseId });
				}
				else
				{
					ViewBag.ErrorMessage = "Hubo un error al actualizar el pedido.";
					return View(updatePurchase);
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar actualizar el pedido: " + ex.Message);
				return View(updatePurchase);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			if (id <= 0)
			{
				return BadRequest("ID de pedido no válido.");
			}

			try
			{
				// Eliminar los detalles de la compra
				var deleteDetailsResponse = await _client.DeleteAsync($"https://localhost:7237/api/Purchase/DeletePurchaseDetails/{id}");
				if (!deleteDetailsResponse.IsSuccessStatusCode)
				{
					ViewBag.ErrorMessage = "Error al eliminar los detalles del pedido.";
					return RedirectToAction("Index");
				}

				// Eliminar la compra principal
				var deletePurchaseResponse = await _client.DeleteAsync($"https://localhost:7237/api/Purchase/DeletePurchase/{id}");
				if (!deletePurchaseResponse.IsSuccessStatusCode)
				{
					ViewBag.ErrorMessage = "Error al eliminar el pedido.";
					return RedirectToAction("Index");
				}

				TempData["SuccessMessage"] = "Pedido eliminado exitosamente.";
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				ViewBag.ErrorMessage = "Error al intentar eliminar el pedido: " + ex.Message;
				return RedirectToAction("Index");
			}
		}








	}
}
