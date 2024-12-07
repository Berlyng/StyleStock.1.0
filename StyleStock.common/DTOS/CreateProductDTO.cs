using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.common.DTOS
{
	public class CreateProductDTO
	{
		public string Name { get; set; } = null!;

		public string Description { get; set; } = null!;

		public decimal Price { get; set; }

		public string Size { get; set; } = null!;

		public string Color { get; set; } = null!;
		[Required]
		public int CategoryId { get; set; }

		public int StockQuantity { get; set; }

		public DateTime EntryDate { get; set; }
	}
}
