using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StyleStock.Common.DTOS;
using StyleStock.domain.DTOS;
using StyleStock.domain.Entities;
using StyleStock.domain.Repository;

namespace StyleStock.api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductController(IProductRepository repo)
        {
            _repo = repo;
        }

        [HttpGet(Name = "GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _repo.GetAllAsync();
                return Ok(products);
            }
            catch (Exception)
            {

                return StatusCode(500, "Ocurrió un error inesperado al obtener los Productos.");
            }
        }

        [HttpPost(Name = "CreateProducts")]
        public async Task<IActionResult> CreateProducts(CreateProductDTO product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newProduct = new Product
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Size = product.Size,
                    Color = product.Color,
                    CategoryId = product.CategoryId,
                    StockQuantity = product.StockQuantity,
                    EntryDate = product.EntryDate,

                };

                await _repo.AddAsync(newProduct);
                return Ok(new { message = "Producto creado con exito", Data = newProduct });
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Ocurrió un error inesperado al crear el producto: {ex.Message}");
            }
        }

        [HttpGet("{id}", Name = "GetProductsyById")]
        public async Task<IActionResult> GetProductsById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("el id no es valido");
            }

            var search = await _repo.GetByIdAsync(id);
            return Ok(search);

        }

            [HttpPut("{id}",Name ="EditProduct")]
        public async Task<IActionResult> EditProduct(int id, UpdateProductDTO product )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updateProduct = await _repo.GetByIdAsync(id);
                if (updateProduct == null)
                {
                    return NotFound();
                }

                updateProduct.Name = product.Name;
                updateProduct.Description = product.Description;
                updateProduct.Price = product.Price;
                updateProduct.Size = product.Size;
                updateProduct.Color = product.Color;
                updateProduct.CategoryId = product.CategoryId;
                updateProduct.StockQuantity = product.StockQuantity;
                updateProduct.EntryDate = product.EntryDate;

                await _repo.UpdateAsync(updateProduct);
                return Ok(new { message = "Producto actualizdo con exito", Data = updateProduct });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error inesperado al actualizar el producto: {ex.Message}");

            }

        }

        [HttpDelete("{id}", Name ="DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _repo.GetByIdAsync(id);
                if (id == null)
                {
                    return NotFound();
                }
                await _repo.DeleteAsync(id);
                return Ok(new { message = "Producto eliminado con exito", Data = product });
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Ocurrió un error inesperado al eliminar el producto: {ex.Message}");
            }
        }
    }
}
