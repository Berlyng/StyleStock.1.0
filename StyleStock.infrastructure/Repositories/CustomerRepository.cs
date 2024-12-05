using StyleStock.domain.Core;
using StyleStock.domain.Entities;
using StyleStock.infrastructure.Core;
using StyleStock.infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.infrastructure.Repositories
{
	public class CustomerRepository:BaseRepository<Customer>, ICustomerRepository
	{
		public CustomerRepository(StyleStockContext context): base(context)
		{
			
		}
	}
}
