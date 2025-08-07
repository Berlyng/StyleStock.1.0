using StyleStock.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.Common.DTOS
{
	public class PurchaseDTO
	{
		public int Id { get; set; }
		public DateTime PurchaseDate { get; set; }
		public decimal TotalAmount { get; set; } 
		public int SupplierId { get; set; }
		public int UserId { get; set; }
	}
}
