using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.domain.DTOS
{
	public class CreateCustomerDTO
	{
		[Required(ErrorMessage = "El campo no puede estar vacio")]
		public string FirstName { get; set; } = null!;
		[Required(ErrorMessage ="El campo no puede estar vacio")]
		public string LastName { get; set; } = null!;
		[Required(ErrorMessage = "El campo no puede estar vacio")]
		[EmailAddress]
		public string Email { get; set; } = null!;
		[Phone]
		[Required(ErrorMessage = "El campo no puede estar vacio")]
		public string Phone { get; set; } = null!;
	}
}
