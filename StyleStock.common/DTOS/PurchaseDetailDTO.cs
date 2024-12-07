using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.common.DTOS
{
	public class PurchaseDetailDTO
	{
		public int DetailId { get; set; }
		public int PurchaseId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal SubTotal { get; set; }
	}

}
