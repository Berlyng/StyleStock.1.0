using Microsoft.EntityFrameworkCore;
using StyleStock.domain.Entities;
using StyleStock.infrastructure.Core;
using StyleStock.infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.infrastructure.Repositories
{
	public class PurchaseDetailRepository : BaseRepository<PurchaseDetail>, IPurchaseDetailRepository
	{
		private readonly StyleStockContext _context;

		public PurchaseDetailRepository(StyleStockContext context) : base(context)
		{
		}

	}
}
