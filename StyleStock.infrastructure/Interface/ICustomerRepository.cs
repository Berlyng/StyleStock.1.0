using StyleStock.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.infrastructure.Interface
{
	public interface ICustomerRepository
	{
		Task<Customer> GetByIdAsync(int id);
		Task<IEnumerable<Customer>> GetAllAsync();
		Task AddAsync(Customer category);
		Task UpdateAsync(Customer category);
		Task DeleteAsync(int id);
	}
}
