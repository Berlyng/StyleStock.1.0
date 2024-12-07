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
	public class PurchaseRepository :BaseRepository<Purchase>, IPurchaseRepository
	{
		private readonly StyleStockContext _context;

		public PurchaseRepository(StyleStockContext context):base(context)
		{
			
		}

	}

}
