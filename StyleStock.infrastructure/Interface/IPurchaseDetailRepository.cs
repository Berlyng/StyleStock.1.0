using StyleStock.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.infrastructure.Interface
{
	public interface IPurchaseDetailRepository
	{
		Task<PurchaseDetail> GetByIdAsync(int id);
		Task<IEnumerable<PurchaseDetail>> GetAllAsync();
		Task AddAsync(PurchaseDetail purchaseDetail);
		Task UpdateAsync(PurchaseDetail purchaseDetail);
		Task DeleteAsync(int id);
	}
}
