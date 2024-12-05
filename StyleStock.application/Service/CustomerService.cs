using StyleStock.application.Interface;
using StyleStock.common.DTOS;
using StyleStock.domain.Entities;
using StyleStock.infrastructure.Interface;
using StyleStock.infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.application.Service
{
	public class CustomerService : ICustomerService
	{
		private readonly ICustomerRepository _customerRepository;

		public CustomerService(ICustomerRepository customerRepository)
		{
			_customerRepository = customerRepository;
		}

		public async Task<IEnumerable<CustomerDTO>> GetAllCustomerAsync()
		{
			try
			{
				var customers = await _customerRepository.GetAllAsync();

				if (customers == null || !customers.Any())
				{
					throw new Exception("No se encontraron los clientes.");
				}

				return customers.Select(c => new CustomerDTO
				{
					CustomerId = c.Id,
					FirstName = c.FirstName,
					LastName = c.LastName,
					Email = c.Email,
					Phone = c.Phone,

				});
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al obtener los clientes: {ex.Message}");
			}
		}

		public async Task<CustomerDTO> GetCustomerByIdAsync(int id)
		{
			try
			{
				var customer = await _customerRepository.GetByIdAsync(id);
				if (customer == null)
				{
					throw new Exception($"Cliente con ID {id} no encontrada.");
				}

				return new CustomerDTO
				{
					CustomerId = customer.Id,
					FirstName = customer.FirstName,
					LastName = customer.LastName,
					Email = customer.Email,
					Phone = customer.Phone,

				};
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al obtener el cliente: {ex.Message}");
			}
		}

		public async Task AddCustomerAsycn(CreateCustomerDTO customerDto)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(customerDto.FirstName))
				{
					throw new Exception("El nombre del cliente es obligatorio.");
				}

				var customer = new Customer
				{
					FirstName = customerDto.FirstName,
					LastName = customerDto.LastName,
					Email = customerDto.Email,
					Phone = customerDto.Phone,

				};

				await _customerRepository.AddAsync(customer);
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al crear los clientes: {ex.Message}");
			}
		}

		public async Task UpdateCustomerAsync(int id, UpdateCustomerDTO customerDto)
		{
			try
			{
				var customer = await _customerRepository.GetByIdAsync(id);
				if (customer == null)
				{
					throw new Exception($"Cliente con ID {id} no encontrada.");
				}

				customer.FirstName = customerDto.FirstName;
				customer.LastName = customerDto.LastName;
				customer.Email = customerDto.Email;
				customer.Phone = customerDto.Phone;
				await _customerRepository.UpdateAsync(customer);
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al actualizar los clientes: {ex.Message}");
			}
		}

		public async Task DeleteCustomerAsync(int id)
		{
			try
			{
				await _customerRepository.DeleteAsync(id);
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al eliminar las categorías: {ex.Message}");
			}
		}
	}
}
