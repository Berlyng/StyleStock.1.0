using StyleStock.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.domain.Repository
{
    public interface IPurchaseRepository
    {
        Task<Purchase> GetByIdAsync(int id);
        Task<IEnumerable<Purchase>> GetAllAsync();
        Task AddAsync(Purchase purchase);
        Task UpdateAsync(Purchase purchase);
        Task DeleteAsync(int id);
    }
}
