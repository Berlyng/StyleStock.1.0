﻿using Microsoft.AspNetCore.Mvc;
using StyleStock.common.DTOS;

namespace StyleStock.web.Controllers
{
	public class CustomerController : Controller
	{
		private readonly HttpClient _client;

		public CustomerController(HttpClient client)
		{
			_client = client;
		}
		public async Task<IActionResult> Index()
		{
			var customers = await _client.GetFromJsonAsync<IEnumerable<CustomerDTO>>("https://localhost:7237/api/Customer/GetAllCustomers");
			if (customers == null)
			{
				ViewBag.ErrorMessage = "No se pudo encontrar las categorias";
				return View();
			}
			return View(customers);
		}
		[HttpGet]
		public IActionResult Create()
		{

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateCustomerDTO newCustomer)
		{
			if (!ModelState.IsValid)
			{
				return View(newCustomer);
			}
			try
			{

				var response = await _client.PostAsJsonAsync("https://localhost:7237/api/Customer/AddCustomer", newCustomer);
				if (response.IsSuccessStatusCode)
				{
					return RedirectToAction("Index");
				}
				else
				{
					ViewBag.ErrorMessage = "Hubo un error al crear la categoria.";
					return View(newCustomer);
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

				var response = await _client.GetAsync($"https://localhost:7237/api/Customer/GetAllCustomersById/{id}");
				if (response == null)
				{
					return NotFound();
				}
				if (response.IsSuccessStatusCode)
				{
					var customer = await response.Content.ReadFromJsonAsync<UpdateCustomerDTO>();
					if (customer == null)
					{
						ViewBag.ErrorMessage = "No se encontraron datos de la Categotira.";
						return RedirectToAction("Index");
					}

					return View(customer);
				}
				else
				{
					ViewBag.ErrorMessage = "Hubo un error al obtener la información.";
					return RedirectToAction("Index");
				}
			}
			catch (Exception)
			{

				ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar actualizar la categoría.");
				return View();
			}

		}

		[HttpPost]
		public async Task<IActionResult> Update(UpdateCustomerDTO customer)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var response = await _client.PutAsJsonAsync($"https://localhost:7237/api/Customer/UpdateCategory/{customer.CustomerId}", customer);

				if (response == null || !response.IsSuccessStatusCode)
				{
					ModelState.AddModelError(string.Empty, "No se pudo actualizar la categoría.");
					return View(customer);
				}

				return RedirectToAction(nameof(Index));

			}
			catch (Exception)
			{

				ModelState.AddModelError(string.Empty, "Ocurrió un error en la base de datos al intentar actualizar la categoría.");
				return View(customer);
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
				var response = await _client.DeleteAsync($"https://localhost:7237/api/Customer/DeleteCustomer/{id}");

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

