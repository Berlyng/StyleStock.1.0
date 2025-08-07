using StyleStock.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.domain.Repository
{
    public interface IPurchaseDetailRepository
    {
	     Task<IEnumerable<PurchaseDetail>> GetByPurchaseIdAsync(int purchaseId);
		Task<PurchaseDetail> GetByIdAsync(int id);
		Task<IEnumerable<PurchaseDetail>> GetAllAsync();
        Task AddAsync(PurchaseDetail detail);
        Task UpdateAsync(PurchaseDetail detail);
        Task DeleteAsync(int id);
    }
}
