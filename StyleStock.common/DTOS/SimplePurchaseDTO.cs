using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.common.DTOS
{
	public class SimplePurchaseDTO
	{
		public int Id { get; set; }
		public DateTime PurchaseDate { get; set; }
		public decimal TotalAmount { get; set; }
	}

}
