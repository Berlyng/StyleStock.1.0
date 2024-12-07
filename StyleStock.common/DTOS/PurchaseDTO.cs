using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.common.DTOS
{
	public class PurchaseDTO
	{
		public int PurchaseId { get; set; }
		public DateTime PurchaseDate { get; set; }
		public decimal TotalAmount { get; set; }
		public int SupplierId { get; set; }
		public int UserId { get; set; }

	}

}
