using StyleStock.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.domain.Repository
{
    public interface ISupplierRepository
    {
        Task<Supplier> GetByIdAsync(int id);
        Task<IEnumerable<Supplier>> GetAllAsync();
        Task AddAsync(Supplier supplier);
        Task UpdateAsync(Supplier supplier);
        Task DeleteAsync(int id);
    }
}
