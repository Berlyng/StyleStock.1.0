using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.common.DTOS
{
	public class CreateCategoryDTO
	{
		[Required]
		public string Name { get; set; } = null!;

		public string Description { get; set; } = null!;
	}
}
