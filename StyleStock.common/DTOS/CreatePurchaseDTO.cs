using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StyleStock.common.DTOS
{
	public class CreatePurchaseDTO
	{
		[JsonIgnore]
		public int PurchaseId { get; set; }

		public DateTime PurchaseDate { get; set; }
		[Required]
		public int SupplierId { get; set; }

		public int UserId { get; set; }

		public List<CreatePurchaseDetailDTO> PurchaseDetails { get; set; } = new List<CreatePurchaseDetailDTO>();

		public decimal TotalAmount => PurchaseDetails.Sum(d => d.SubTotal) * 1.18m; // Calcula el total con impuesto
	}


}
