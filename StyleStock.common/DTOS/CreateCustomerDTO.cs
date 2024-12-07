using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.common.DTOS
{
	public class CreateCustomerDTO
	{
		public string FirstName { get; set; } = null!;

		public string LastName { get; set; } = null!;
		[Required]
		public string Email { get; set; } = null!;

		public string Phone { get; set; } = null!;
	}
}
