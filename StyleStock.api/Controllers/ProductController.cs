using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StyleStock.application.Interface;
using StyleStock.application.Service;
using StyleStock.common.DTOS;

namespace StyleStock.api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpGet(Name ="GetAllProduct")]
		public async Task<IActionResult> GetAllProduct()
		{
			try
			{
				var products = await _productService.GetAllProductAsync();
				return Ok(products);
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{id}", Name ="GetProductById")]
		public async Task<IActionResult> GetProductById(int id)
		{
			try
			{
				var product = await _productService.GetProductByIdAsync(id);
				return Ok(product);
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost(Name ="AddProduct")]
		public async Task<IActionResult> AddProduct(CreateProductDTO productDto)
		{
			try
			{
				await _productService.AddProductAsycn(productDto);
				return Ok(new { Message = "Producto creado exitosamente", Data = productDto });
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpPut("{id}", Name ="UpdateProduct")]
		public async Task<IActionResult> UpdateProduct(int id, UpdateProductDTO productDto)
		{
			try
			{
				await _productService.UpdateProductAsync(id, productDto);
				return NoContent();
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete("{id}", Name ="DeleteProduct")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			try
			{
				await _productService.DeleteProductAsync(id);
				return NoContent();
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

	}
}
