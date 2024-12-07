using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.common.DTOS
{
	public class SaleDTO
	{
		public int SaleId { get; set; }

		public DateTime SaleDate { get; set; }

		public decimal TotalAmount { get; set; }

		public int CustomerId { get; set; }

		public int UserId { get; set; }
	}
}
