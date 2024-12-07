using StyleStock.common.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.application.Interface
{
	public interface ISaleService
	{
		Task<IEnumerable<SaleDTO>> GetAllSalesAsync();
		Task<SaleDTO> GetSaleByIdAsync(int id);
		Task AddSaleAsync(CreateSaleDTO sale);
		Task UpdateSaleAsync(int id, UpdateSaleDTO sale);
		Task DeleteSaleAsync(int id);
	}
}
