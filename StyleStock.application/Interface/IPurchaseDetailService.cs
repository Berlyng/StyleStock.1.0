using StyleStock.common.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.application.Interface
{
	public interface IPurchaseDetailService
	{
		Task<PurchaseDetailDTO> GetPurchaseDetailByIdAsync(int id);
		Task<IEnumerable<PurchaseDetailDTO>> GetAllPurchaseDetailAsync();
		Task AddPurchaseDetailAsycn(CreatePurchaseDetailDTO detail);
		Task UpdatePurchaseDetailAsync(int id, UpdatePurchaseDetailDTO product);
		Task DeletePurchaseDetailAsync(int id);
	}


}
