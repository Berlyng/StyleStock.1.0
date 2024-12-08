using StyleStock.common.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.application.Interface
{
	public interface IPurchaseService
	{
		Task<PurchaseDTO> GetPurchaseByIdAsync(int id);
		Task<IEnumerable<PurchaseDTO>> GetAllPurchaseAsync();
		Task AddPurchaseAsync(CreatePurchaseDTO purchase);
		Task UpdatePurchaseAsync(int id, UpdatePurchaseDTO purchaseDTO);
		Task DeletePurchaseAsync(int id);
	}

}
