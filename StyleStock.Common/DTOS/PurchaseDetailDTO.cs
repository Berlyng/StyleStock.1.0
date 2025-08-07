using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StyleStock.Common.DTOS
{
	public class PurchaseDetailDTO
	{
		
		[JsonPropertyName("Id")]
		public int PurchaseId { get; set; }

		public int ProductId { get; set; }

		public int Quantity { get; set; }

		public decimal UnitPrice { get; set; }

		public decimal SubTotal { get; set; }
	}
}
