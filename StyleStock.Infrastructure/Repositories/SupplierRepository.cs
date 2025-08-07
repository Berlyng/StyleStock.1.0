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
    public class SupplierRepository:BaseRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(StyleStockContext context):base(context)
        {
            
        }
    }
}
