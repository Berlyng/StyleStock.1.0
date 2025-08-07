using Microsoft.EntityFrameworkCore;
using StyleStock.domain;
using StyleStock.domain.Core;
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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(StyleStockContext context) : base(context)
        {
        }
    }

}
