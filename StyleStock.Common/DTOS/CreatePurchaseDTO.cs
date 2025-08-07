using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.Common.DTOS
{
	public class CreatePurchaseDTO
	{
		
		public DateTime PurchaseDate { get; set; }
		public int SupplierID { get; set; }
		public int UserId { get; set; }

		public required List<CreatePurchaseDetailDTO> PurchaseDetails { get; set; }
	}
}
