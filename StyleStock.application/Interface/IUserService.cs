using StyleStock.common.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.application.Interface
{
	public interface IUserService
	{
		Task<UserDTO> GetUserByIdAsync(int id);
		Task<IEnumerable<UserDTO>> GetAllUserAsync();
		Task AddUserAsycn(CreateUserDTO user);
		Task UpdateUserAsync(int id, UpdateUserDTO user);
		Task DeleteUserAsync(int id);
	}
}
