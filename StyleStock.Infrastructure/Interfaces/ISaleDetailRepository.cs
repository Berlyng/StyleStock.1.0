using StyleStock.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.domain.Repository
{
    public interface ISaleDetailRepository
    {
        Task<SalesDetail> GetByIdAsync(int id);
        Task<IEnumerable<SalesDetail>> GetAllAsync();
        Task AddAsync(SalesDetail salesDetail);
        Task UpdateAsync(SalesDetail salesDetail);
        Task DeleteAsync(int id);
    }
}
