using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StyleStock.Common.DTOS;
using StyleStock.domain.Entities;
using StyleStock.domain.Repository;

namespace StyleStock.api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserRepository _repo;

		public UserController(IUserRepository repo)
		{
			_repo = repo;
		}


		[HttpGet(Name = "GetUsers")]
		public async Task<IActionResult> GetUsers()
		{
			try
			{
				var supplier = await _repo.GetAllAsync();
				return Ok(supplier);
			}
			catch (Exception)
			{

				return StatusCode(500, "Ocurrió un error inesperado al obtener los Usuarios.");
			}
		}


		[HttpPost(Name = "CreateUser")]
		public async Task<IActionResult> CreateUser(CreateUserDTO user)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				var newUser = new User
				{
					FirstName = user.FirstName,
					LastName = user.LastName,
					Email = user.Email,
					PasswordHash = user.PasswordHash,
					Role = user.Role,

				};

				await _repo.AddAsync(newUser);
				return Ok(new { message = "Producto creado con exito", Data = newUser });
			}
			catch (Exception ex)
			{

				return StatusCode(500, $"Ocurrió un error inesperado al crear el producto: {ex.Message}");
			}
		}
		[HttpGet("{id}", Name = "GetUserById")]
		public async Task<IActionResult> GetUserById(int id)
		{
			if (id <= 0)
			{
				return BadRequest("el id no es valido");
			}

			var search = await _repo.GetByIdAsync(id);
			return Ok(search);

		}

		[HttpPut("{id}", Name = "EditUser")]
		public async Task<IActionResult> EditUser(int id, UpdateUserDTO user)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				var updateUser = await _repo.GetByIdAsync(id);
				if (updateUser == null)
				{
					return NotFound();
				}

				updateUser.FirstName = user.FirstName;
				updateUser.LastName = user.LastName;
				updateUser.Email = user.Email;
				updateUser.PasswordHash = user.PasswordHash;
				updateUser.Role = user.Role;


				await _repo.UpdateAsync(updateUser);
				return Ok(new { message = "Producto actualizdo con exito", Data = updateUser });
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Ocurrió un error inesperado al actualizar el producto: {ex.Message}");

			}

		}

		[HttpDelete("{id}", Name = "DeleteUser")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			try
			{
				var product = await _repo.GetByIdAsync(id);
				if (id == null)
				{
					return NotFound();
				}
				await _repo.DeleteAsync(id);
				return Ok(new { message = "Producto eliminado con exito", Data = product });
			}
			catch (Exception ex)
			{

				return StatusCode(500, $"Ocurrió un error inesperado al eliminar el producto: {ex.Message}");
			}
		}
	}


}
