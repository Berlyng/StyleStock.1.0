using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using StyleStock.application.Interface;
using StyleStock.application.Service;
using StyleStock.common.DTOS;
using System.Drawing.Text;

namespace StyleStock.api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class SupplierController : ControllerBase
	{
		private readonly ISupplierService _supplierService;

		public SupplierController(ISupplierService supplierService)
		{
			_supplierService = supplierService;
		}

		[HttpGet(Name = "GetAllSupplier")]
		public async Task<IActionResult> GetAllSupplier()
		{
			try
			{
				var suppliers = await _supplierService.GetAllSupplierAsync();
				return Ok(suppliers);
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{id}", Name = "GetAllSupplierById")]
		public async Task<IActionResult> GetAllSupplierById(int id)
		{
			try
			{
				var supplier = await _supplierService.GetSupplierByIdAsync(id);
				return Ok(supplier);
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost("AddSupplier")]
		public async Task<IActionResult> AddSupplier(CreateSupplierDTO supplierDto)
		{
			try
			{
				await _supplierService.AddSupplierAsycn(supplierDto);
				return Ok(new { Message = "Suplidor creado exitosamente", Data = supplierDto });
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpPut("{id}", Name ="UpdateSupplier")]
		public async Task<IActionResult> UpdateSupplier(int id, UpdateSupplierDTO supplierDto)
		{
			try
			{
				await _supplierService.UpdateSupplierAsync(id, supplierDto);
				return NoContent();
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete("{id}", Name ="DeleteSupplier")]
		public async Task<IActionResult> DeleteSupplier(int id) 
		{
			try
			{
				await _supplierService.DeleteSupplierAsync(id);
				return NoContent();
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}
	}
}
