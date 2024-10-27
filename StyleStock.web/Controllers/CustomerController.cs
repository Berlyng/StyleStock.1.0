using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StyleStock.domain;
using StyleStock.domain.DTOS;
using StyleStock.domain.Entities;

namespace StyleStock.web.Controllers
{
	public class CustomerController : Controller
	{
		private readonly StyleStockContext _context;

		public CustomerController(StyleStockContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			var customers = _context.Customers.Select(c => new CustomerDTO
			{
				CustomerId = c.CustomerId,
				FirstName = c.FirstName,
				LastName = c.LastName,
				Email = c.Email,
				Phone = c.Phone,
			}).ToList();

			return View(customers);
		}
		[HttpGet]
		public IActionResult Create()
		{

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateCustomerDTO customer)
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

			_context.Customers.Add(newCustomer);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));


		}

		[HttpGet]
		public async Task<IActionResult> Update(int id)
		{
			if (id <= 0)
			{
				return BadRequest("El ID no existe");
			}

			var search = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
			if (search == null)
			{
				return NotFound();
			}

			var customer = new UpdateCustomerDTO
			{
				CustomerId = search.CustomerId,
				FirstName = search.FirstName,
				LastName = search.LastName,
				Email = search.Email,
				Phone = search.Phone,
			};

			return View(customer);
		}

		[HttpPost]
		public async Task<IActionResult> Update(UpdateCustomerDTO customer)
		{
			
			if (!ModelState.IsValid)
			{
				return View(customer);
			}
			try
			{
				var updateCustomer = await _context.Customers.FindAsync(customer.CustomerId);
				if (updateCustomer == null)
				{
					return NotFound();
				}

				updateCustomer.FirstName = customer.FirstName;
				updateCustomer.LastName = customer.LastName;
				updateCustomer.Email = customer.Email;
				updateCustomer.Phone = customer.Phone;

				_context.Customers.Update(updateCustomer);
				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));

			}
			catch (Exception)
			{

				ModelState.AddModelError(string.Empty, "Ocurrió un error en la base de datos al intentar actualizar la categoría.");
				return View(customer);
			}

		}
		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
				if (customer == null)
				{
					return NotFound();
				}

				_context.Customers.Remove(customer);
				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}
			catch (Exception)
			{

				ModelState.AddModelError(string.Empty, "Ocurrió un error en la base de datos al intentar Eliminar la categoría.");
				return View();
			}
		}
	}
}
