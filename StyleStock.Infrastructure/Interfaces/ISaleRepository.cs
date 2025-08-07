using StyleStock.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.domain.Repository
{
    public interface ISaleRepository
    {
        Task<Sale> GetByIdAsync(int id);
        Task<IEnumerable<Sale>> GetAllAsync();
        Task AddAsync(Sale sale);
        Task UpdateAsync(Sale sale);
        Task DeleteAsync(int id);
    }
}
