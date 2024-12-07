using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.common.DTOS
{
	public class UpdatePurchaseDTO
	{
		public DateTime PurchaseDate { get; set; }
		public int SupplierId { get; set; }
		public int UserId { get; set; }
		public List<UpdatePurchaseDetailDTO> Details { get; set; } = new List<UpdatePurchaseDetailDTO>();
	}
}
