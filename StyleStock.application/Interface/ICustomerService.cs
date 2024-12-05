using StyleStock.common.DTOS;
using StyleStock.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.application.Interface
{
	public interface ICustomerService
	{
		Task<CustomerDTO> GetCustomerByIdAsync(int id);
		Task<IEnumerable<CustomerDTO>> GetAllCustomerAsync();
		Task AddCustomerAsycn(CreateCustomerDTO customer);
		Task UpdateCustomerAsync(int id,UpdateCustomerDTO customer);
		Task DeleteCustomerAsync(int id);
	}
}
