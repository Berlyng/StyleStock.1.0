using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.common.DTOS
{
	public class SalesDetailDTO
	{
		public int DetailId { get; set; }

		public int SaleId { get; set; }

		public int ProductId { get; set; }

		public int Quantity { get; set; }

		public decimal UnitPrice { get; set; }

		public decimal SubTotal { get; set; }
	}
}
