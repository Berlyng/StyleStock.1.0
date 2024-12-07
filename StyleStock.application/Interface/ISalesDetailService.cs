using StyleStock.common.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.application.Interface
{
	public interface ISalesDetailService
	{
		Task<IEnumerable<SalesDetailDTO>> GetAllSalesAsync();
		Task<SalesDetailDTO> GetSaleByIdAsync(int id);
		Task AddSaleAsync(CreateSalesDetailDTO detail);
		Task UpdateSaleAsync(int id, UpdateSalesDetailDTO detail);
		Task DeleteSaleAsync(int id);
	}
}
