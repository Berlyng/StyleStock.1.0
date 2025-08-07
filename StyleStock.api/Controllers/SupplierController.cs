using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StyleStock.Common.DTOS;
using StyleStock.domain.DTOS;
using StyleStock.domain.Entities;
using StyleStock.domain.Repository;

namespace StyleStock.api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SupplierController : ControllerBase
	{
		private readonly ISupplierRepository _repo;

		public SupplierController(ISupplierRepository repo)
		{
			_repo = repo;
		}

		[HttpGet(Name = "GetSuppliers")]
		public async Task<IActionResult> GetSuppliers()
		{
			try
			{
				var supplier = await _repo.GetAllAsync();
				return Ok(supplier);
			}
			catch (Exception)
			{

				return StatusCode(500, "Ocurrió un error inesperado al obtener los Productos.");
			}
		}

		[HttpPost(Name = "CreateSupplier")]
		public async Task<IActionResult> CreateSupplier(CreateSupplierDTO supplier)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				var newSupplier = new Supplier
				{
					Name = supplier.Name,
					Phone = supplier.Phone,
					Email = supplier.Email,
					Adress = supplier.Adress,

				};

				await _repo.AddAsync(newSupplier);
				return Ok(new { message = "Producto creado con exito", Data = newSupplier });
			}
			catch (Exception ex)
			{

				return StatusCode(500, $"Ocurrió un error inesperado al crear el producto: {ex.Message}");
			}
		}

		[HttpGet("{id}", Name = "GetSuppliersyById")]
		public async Task<IActionResult> GetSuppliersyById(int id)
		{
			if (id <= 0)
			{
				return BadRequest("el id no es valido");
			}

			var search = await _repo.GetByIdAsync(id);
			return Ok(search);

		}

		[HttpPut("{id}", Name = "EditSupplier")]
		public async Task<IActionResult> EditSupplier(int id, UpdateSupplierDTO supplier)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				var updateSupplier = await _repo.GetByIdAsync(id);
				if (updateSupplier == null)
				{
					return NotFound();
				}

				updateSupplier.Name = supplier.Name;
				updateSupplier.Phone = supplier.Phone;
				updateSupplier.Email = supplier.Email;
				updateSupplier.Adress = supplier.Adress;

				await _repo.UpdateAsync(updateSupplier);
				return Ok(new { message = "Producto actualizdo con exito", Data = updateSupplier });
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Ocurrió un error inesperado al actualizar el producto: {ex.Message}");

			}

		}

		[HttpDelete("{id}", Name = "DeleteSupplier")]
		public async Task<IActionResult> DeleteSupplier(int id)
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
