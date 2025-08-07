using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StyleStock.domain;
using StyleStock.domain.DTOS;
using StyleStock.domain.Entities;
using StyleStock.domain.Repository;
using StyleStock.Infrastructure.Repositories;

namespace StyleStock.api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
        private readonly ICustomerRepository _repo;

        public CustomerController(ICustomerRepository repo)
		{
            _repo = repo;
        }

		[HttpGet(Name = "GetCustomers")]
		public async Task <IActionResult> GetCustomers()
		{
			var customers = await _repo.GetAllAsync();

			return Ok(customers);
		}


		[HttpPost(Name = "CreateCustomer")]
		public async Task<IActionResult> CreateCustomer(CreateCustomerDTO customer)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var newCustomer = new Customer
			{
				FirstName = customer.FirstName,
				LastName = customer.LastName,
				Email = customer.Email,
				Phone = customer.Phone,
			};

			await _repo.AddAsync(newCustomer);

			
			return Ok(new { message = "Cliente creado con exito", Data = newCustomer });

		}


		[HttpGet("{id}", Name = "GetCustomeryById")]
		public async Task<IActionResult> GetCustomerById(int id)
		{
			if (id <= 0)
			{
				return BadRequest("el id no es valido");
			}

			var search = await _repo.GetByIdAsync(id);
			return Ok(search);
		}


		[HttpPut("{CustomerId}", Name = "UpdateCustomer")]
		public async Task<IActionResult> UpdateCustomer(int CustomerId, UpdateCustomerDTO customer)
		{

			if (!ModelState.IsValid)
			{
				return Ok(customer);
			}
			try
			{
				var updateCustomer = await _repo.GetByIdAsync(CustomerId);
				if (updateCustomer == null)
				{
					return NotFound();
				}

				updateCustomer.FirstName = customer.FirstName;
				updateCustomer.LastName = customer.LastName;
				updateCustomer.Email = customer.Email;
				updateCustomer.Phone = customer.Phone;

				await _repo.UpdateAsync(updateCustomer);

				return Ok(new { message = "Cliente actualizdo con exito", Data = updateCustomer });

			}
			catch (Exception)
			{

				return StatusCode(500, "Ocurrió un error inesperado al obtener los Clientes.");

			}
		}

		[HttpDelete("{id}",Name ="DeleteCustomer")]
		public async Task<IActionResult> DeleteCustomer(int id)
		{
			try
			{
				var customer = await _repo.GetByIdAsync(id);
				if (customer == null)
				{
					return NotFound();
				}

				await _repo.DeleteAsync(id);

				return Ok(new {message = "El cliente fue eliminado con exito", Data = customer});
			}
			catch (Exception)
			{

				return StatusCode(500, "Ocurrió un error en la base de datos al intentar eliminar el cliente.");
			}
		}
	}
}
