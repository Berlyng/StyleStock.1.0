using StyleStock.common.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.application.Interface
{
	public interface ISupplierService
	{
		Task<SupplierDTO> GetSupplierByIdAsync(int id);
		Task<IEnumerable<SupplierDTO>> GetAllSupplierAsync();
		Task AddSupplierAsycn(CreateSupplierDTO supplier);
		Task UpdateSupplierAsync(int id, UpdateSupplierDTO supplier);
		Task DeleteSupplierAsync(int id);
	}
}
