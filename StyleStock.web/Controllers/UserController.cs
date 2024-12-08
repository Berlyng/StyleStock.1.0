using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StyleStock.common.DTOS;

namespace StyleStock.web.Controllers
{
	public class UserController : Controller
	{
		private readonly HttpClient _client;

		public UserController(HttpClient client)
		{
			_client = client;
		}
		public async Task<IActionResult> Index()
		{
			var categories = await _client.GetFromJsonAsync<IEnumerable<UserDTO>>("https://localhost:7237/api/User/GetAllUser");
			if (categories == null)
			{
				ViewBag.ErrorMessage = "No se pudo encontrar las categorias";
				return View();
			}
			return View(categories);
		}

		[HttpGet]
		public IActionResult Create()
		{
			ViewBag.Roles = new SelectList(new List<string> { "Admin", "Usuario" });
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(UpdateUserDTO newUser)
		{
			if (!ModelState.IsValid)
			{
				return View(newUser);
			}
			try
			{

				var response = await _client.PostAsJsonAsync("https://localhost:7237/api/User/AddUser", newUser);
				if (response.IsSuccessStatusCode)
				{
					return RedirectToAction("Index");
				}
				else
				{
					ViewBag.ErrorMessage = "Hubo un error al crear el usuario.";
					return View(newUser);
				}
			}
			catch (Exception)
			{

				ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar crear el usuario.");
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
				

				var response = await _client.GetAsync($"https://localhost:7237/api/User/GetUserById/{id}");
				if (response == null)
				{
					return NotFound();
				}
				if (response.IsSuccessStatusCode)
				{
					var user = await response.Content.ReadFromJsonAsync<UpdateUserDTO>();
					if (user == null)
					{
						ViewBag.ErrorMessage = "No se encontraron datos del usuario.";
						return RedirectToAction("Index");
					}
					ViewBag.Roles = new SelectList(new List<string> { "Admin", "Usuario" });
					return View(user);
				}
				else
				{
					ViewBag.ErrorMessage = "Hubo un error al obtener la información.";
					return RedirectToAction("Index");
				}

			}
			catch (Exception)
			{

				ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar actualizar el usuario.");
				return View();
			}

		}

		[HttpPost]
		public async Task<IActionResult> Update(UpdateUserDTO newUser)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var response = await _client.PutAsJsonAsync($"https://localhost:7237/api/User/UpdateUser/{newUser.UserId}", newUser);

				if (response == null || !response.IsSuccessStatusCode)
				{
					ModelState.AddModelError(string.Empty, "No se pudo actualizar el usuario.");
					return View(newUser);
				}

				return RedirectToAction(nameof(Index));

			}
			catch (Exception)
			{

				ModelState.AddModelError(string.Empty, "Ocurrió un error en la base de datos al intentar actualizar el usuario.");
				return View(newUser);
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
				var response = await _client.DeleteAsync($"https://localhost:7237/api/User/DeleteUser/{id}");

				if (response.IsSuccessStatusCode)
				{
					TempData["SuccessMessage"] = "el usuario se eliminó correctamente.";
				}
				else
				{
					var errorContent = await response.Content.ReadAsStringAsync();
					TempData["ErrorMessage"] = $"Hubo un error al eliminar el usuario: {errorContent}";
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
