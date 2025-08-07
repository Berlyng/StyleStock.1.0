using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.Common.DTOS
{
	public class UpdatePurchaseDTO:PurchaseDTO
	{
		public List<UpdatePurchaseDetailDTO> PurchaseDetails { get; set; }
	}
}
