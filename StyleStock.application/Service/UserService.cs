using StyleStock.application.Interface;
using StyleStock.common.DTOS;
using StyleStock.domain.Entities;
using StyleStock.infrastructure.Interface;
using StyleStock.infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.application.Service
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task AddUserAsycn(CreateUserDTO userDto)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(userDto.FirstName))
				{
					throw new Exception("El nombre del usuario es obligatorio.");
				}

				var supplier = new User
				{
					FirstName = userDto.FirstName,
					LastName = userDto.LastName,
					Email = userDto.Email,
					PasswordHash = userDto.PasswordHash,
					Role = userDto.Role,
				};

				await _userRepository.AddAsync(supplier);
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al crear los usuarios: {ex.Message}");
			}
		}

		public async Task DeleteUserAsync(int id)
		{
			try
			{
				await _userRepository.DeleteAsync(id);
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al eliminar los usuarios: {ex.Message}");
			}
		}

		public async Task<IEnumerable<UserDTO>> GetAllUserAsync()
		{
			try
			{
				var users = await _userRepository.GetAllAsync();
				if (users == null || !users.Any())
				{
					throw new Exception("No se encontraron los usuarios.");
				}

				return users.Select(p => new UserDTO
				{
					UserId = p.Id,
					FirstName = p.FirstName,
					LastName = p.LastName,
					Email = p.Email,
					PasswordHash = p.PasswordHash,
					Role = p.Role,

				});

			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al obtener los usuarios: {ex.Message}");
			}
		}

		public async Task<UserDTO> GetUserByIdAsync(int id)
		{
			try
			{
				var user = await _userRepository.GetByIdAsync(id);
				if (user == null)
				{
					throw new Exception($"user con ID {id} no encontrada.");
				}

				return new UserDTO
				{
					UserId=user.Id,
					FirstName = user.FirstName,
					LastName = user.LastName,
					Email = user.Email,
					PasswordHash = user.PasswordHash,
					Role = user.Role,
				
				};
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al obtener el usuario: {ex.Message}");
			}
		}

		public async Task UpdateUserAsync(int id, UpdateUserDTO userDto)
		{
			try
			{
				var user = await _userRepository.GetByIdAsync(id);
				if (user == null)
				{
					throw new Exception($"Usuario con ID {id} no encontrada.");
				}
				user.FirstName = userDto.FirstName;
				user.LastName = userDto.LastName;
				user.Email = userDto.Email;
				user.PasswordHash = userDto.PasswordHash;
				user.Role = userDto.Role;


				await _userRepository.UpdateAsync(user);
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al actualizar el usuario: {ex.Message}");
			}
		}
	}
}
