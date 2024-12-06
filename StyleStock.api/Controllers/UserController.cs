using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StyleStock.application.Interface;
using StyleStock.common.DTOS;

namespace StyleStock.api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet(Name ="GetAllUser")]
		public async Task<IActionResult> GetAllUser()
		{
			try
			{
				var users= await _userService.GetAllUserAsync();
				return Ok(users);
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{id}", Name ="GetUserById")]
		public async Task<IActionResult> GetUserById(int id)
		{
			try
			{
				var user = await _userService.GetUserByIdAsync(id);
				return Ok(user);
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost(Name ="AddUser")]
		public async Task<IActionResult> AddUser(CreateUserDTO userDto)
		{
			try
			{
				await _userService.AddUserAsycn(userDto);
				return Ok(new { Message = "Usuario creado exitosamente", Data = userDto });
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpPut("{id}", Name ="UpdateUser")]
		public async Task<IActionResult> UpdateUser(int id, UpdateUserDTO userDto)
		{
			try
			{
				await _userService.UpdateUserAsync(id, userDto);
				return Ok("Usuario Actualizado con exito");
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete("{id}", Name ="DeleteUser")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			try
			{
				await _userService.DeleteUserAsync(id);
				return Ok("Usuario eliminado con exito");
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message); 
			}
		}
	}
}
