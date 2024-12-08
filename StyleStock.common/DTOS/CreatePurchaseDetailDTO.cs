using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StyleStock.common.DTOS
{
	public class CreatePurchaseDetailDTO
	{
		[JsonIgnore]
		public int PurchaseId { get; set; }
		[Required]
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }

		public decimal SubTotal => Quantity * UnitPrice;
	}


}
