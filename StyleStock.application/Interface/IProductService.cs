using StyleStock.common.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.application.Interface
{
	public interface IProductService
	{
		Task<ProductDTO> GetProductByIdAsync(int id);
		Task<IEnumerable<ProductDTO>> GetAllProductAsync();
		Task AddProductAsycn(CreateProductDTO product);
		Task UpdateProductAsync(int id, UpdateProductDTO product);
		Task DeleteProductAsync(int id);
	}
}
