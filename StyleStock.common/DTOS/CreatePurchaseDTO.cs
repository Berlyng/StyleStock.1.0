using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.common.DTOS
{
	public class CreatePurchaseDTO
	{
		public DateTime PurchaseDate { get; set; }
		[Required]
		public int SupplierId { get; set; }
		[Required]
		public int UserId { get; set; }
	}


}
