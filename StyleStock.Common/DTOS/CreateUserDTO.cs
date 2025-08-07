using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.Common.DTOS
{
	public class CreateUserDTO
	{
		public string FirstName { get; set; } = null!;

		public string LastName { get; set; } = null!;

		public string Email { get; set; } = null!;

		public string PasswordHash { get; set; } = null!;

		public string Role { get; set; } = null!;
	}
}
