using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.common.DTOS
{
	public class CategoryDTO
	{
		public int CategoryId { get; set; }

		public string Name { get; set; } = null!;

		public string Description { get; set; } = null!;
	}
}
