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
	public class CategoryRepository: BaseRepository<Category>, ICategoryRepository
	{
		public CategoryRepository(StyleStockContext context) : base(context)
		{
		}
	}
}
