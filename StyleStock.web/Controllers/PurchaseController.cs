using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StyleStock.common.DTOS;
using StyleStock.web.Models;

namespace StyleStock.web.Controllers
{
	public class PurchaseController : Controller
	{
		private readonly HttpClient _client;

		public PurchaseController(HttpClient client)
		{
			_client = client;
		}
		public async Task<IActionResult> Index()
		{
			try
			{
				var purchases = await _client.GetFromJsonAsync<IEnumerable<PurchaseDTO>>("https://localhost:7237/api/Purchase/GetAllPurchase");

				if (purchases == null || !purchases.Any())
				{
					ViewBag.ErrorMessage = "No hay pedidos disponibles. Por favor, crea un nuevo pedido.";
					return View(new List<PurchaseDTO>());
				}

				var suppliers = await _client.GetFromJsonAsync<IEnumerable<SupplierDTO>>("https://localhost:7237/api/Supplier/GetAllSupplier");
				if (suppliers == null)
				{
					ViewBag.ErrorMessage = "No se pudo encontrar los suplidores.";
					return View(suppliers);
				}

				var PurchaseWithSupplierNames = purchases.Select(product =>
				{
					var supplier = suppliers.FirstOrDefault(c => c.SupplierId == product.SupplierId);
					product.SupplierName = supplier?.Name ?? "Supplier no encontrada";
					return product;


				});
				return View(PurchaseWithSupplierNames);
			}
			catch (Exception ex)
			{
				ViewBag.ErrorMessage = $"Error al cargar los pedidos: {ex.Message}";
				return View(new List<PurchaseDTO>());
			}
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{

			var purchases = await _client.GetFromJsonAsync<IEnumerable<PurchaseDTO>>("https://localhost:7237/api/Purchase/GetAllPurchase");
			if (purchases == null || !purchases.Any())
			{
				ViewBag.ErrorMessage = "No se encontraron compras.";
				return View();
			}

			var suppliers = await _client.GetFromJsonAsync<IEnumerable<SupplierDTO>>("https://localhost:7237/api/Supplier/GetAllSupplier");
			if (suppliers == null || !suppliers.Any())
			{
				ViewBag.ErrorMessage = "No se encontraron proveedores.";
				return View(purchases);
			}

			// Asocia cada compra con el proveedor correspondiente
			var purchasesWithSuppliers = purchases.Select(purchase =>
			{
				var supplier = suppliers.FirstOrDefault(s => s.SupplierId == purchase.SupplierId);
				return new
				{
					Purchase = purchase,
					SupplierName = supplier?.Name ?? "Proveedor no encontrado"
				};
			}).ToList();

			// Pasar los datos a la vista
			return View(purchasesWithSuppliers);

		}

		[HttpPost]
		public async Task<IActionResult> Create(CreatePurchaseDTO newPurchase)
		{
			if (!ModelState.IsValid)
			{
				// Obtener proveedores y productos nuevamente en caso de error de validación
				return View(newPurchase);
			}

			try
			{
				var response = await _client.PostAsJsonAsync("https://localhost:7237/api/Purchase/AddPurchase", newPurchase);
				if (response.IsSuccessStatusCode)
				{
					var createdPurchase = await response.Content.ReadFromJsonAsync<CreatePurchaseDTO>();
					TempData["PurchaseId"] = createdPurchase.PurchaseId;
					return RedirectToAction("CreatePurchaseDetail", "PurchaseDetail");
				}
				else
				{
					TempData["DebugMessage"] = "Error del API: " + response.StatusCode;
					ViewBag.ErrorMessage = "Hubo un error al crear el pedido.";
					return View(newPurchase);
				}
			}
			catch (Exception ex)
			{
				TempData["DebugMessage"] = "Excepción al crear compra: " + ex.Message;
				ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar crear el pedido: " + ex.Message);
				return View(newPurchase);
			}
		}

		[HttpGet]
		public async Task<IActionResult> Update(int id)
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
		public async Task<IActionResult> Update(UpdatePurchaseDTO updatePurchase)
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
					return RedirectToAction("UpdatePurchaseDeail", new { purchaseId = updatePurchase.PurchaseId });
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




	}
}
