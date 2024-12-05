using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StyleStock.application.Interface;
using StyleStock.application.Service;
using StyleStock.common.DTOS;

namespace StyleStock.api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly ICustomerService _customerService;

		public CustomerController(ICustomerService customerService)
		{
			_customerService = customerService;
		}

		[HttpGet(Name ="GetAllCustomers")]
		public async Task<IActionResult> GetAllCustomers()
		{
			try
			{
				var customers = await _customerService.GetAllCustomerAsync();
				return Ok(customers);
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{id}", Name = "GetAllCustomersById")]
		public async Task<IActionResult> GetAllCustomersById(int id)
		{
			try
			{
				var customers = await _customerService.GetCustomerByIdAsync(id);
				return Ok(customers);
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost(Name = "AddCustomer")]
		public async Task<IActionResult> AddCustomer(CreateCustomerDTO customerDto)
		{
			try
			{
				await _customerService.AddCustomerAsycn(customerDto);
				return Ok(new { Message = "Cliente cread exitosamente", Data = customerDto });
			}
			catch (Exception ex)
			{
				return BadRequest(new { Error = ex.Message });
			}
		}

		[HttpPut("{id}", Name = "UpdateCustomer")]
		public async Task<IActionResult> UpdateCustomer(int id, UpdateCustomerDTO customerDto)
		{
			try
			{
				await _customerService.UpdateCustomerAsync(id, customerDto);
				return NoContent();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{id}", Name ="DeleteCustomer")]
		public async Task<IActionResult> DeleteCustomer(int id)
		{
			try
			{
				await _customerService.DeleteCustomerAsync(id);
				return NoContent();
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

	}
}
