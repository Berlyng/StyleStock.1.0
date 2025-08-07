using Microsoft.EntityFrameworkCore;
using StyleStock.domain;
using StyleStock.domain.Entities;
using StyleStock.domain.Repository;
using StyleStock.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.Infrastructure.Repositories
{
	public class PurchaseDetailRepository : BaseRepository<PurchaseDetail>, IPurchaseDetailRepository
	{
		private readonly DbSet<PurchaseDetail> _dbSet;

		public PurchaseDetailRepository(StyleStockContext context) : base(context)
		{
			_dbSet = context.Set<PurchaseDetail>(); 
		}

		public async Task<IEnumerable<PurchaseDetail>> GetByPurchaseIdAsync(int purchaseId)
		{
			
			var details = await _dbSet.Where(detail => detail.PurchaseID == purchaseId).ToListAsync();
			return details ?? Enumerable.Empty<PurchaseDetail>();
		}
	}

}
