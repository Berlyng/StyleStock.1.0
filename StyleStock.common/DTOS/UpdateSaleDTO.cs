using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.common.DTOS
{
	public class UpdateSaleDTO
	{
		public DateTime SaleDate { get; set; }

		public int CustomerId { get; set; }

		public int UserId { get; set; }
	}
}
