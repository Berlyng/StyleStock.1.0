using StyleStock.domain;
using StyleStock.domain.Entities;
using StyleStock.domain.Repository;
using StyleStock.Infrastructure.Core;

namespace StyleStock.Infrastructure.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(StyleStockContext context) : base(context)
        {
        }
    }
}
