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
	public class SupplierService:ISupplierService
	{
		private readonly ISupplierRepository _supplierRepository;

		public SupplierService(ISupplierRepository supplierRepository)
		{
			_supplierRepository = supplierRepository;
		}

		public async Task AddSupplierAsycn(CreateSupplierDTO supplierDto)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(supplierDto.Name))
				{
					throw new Exception("El nombre del suplidor es obligatorio.");
				}

				var supplier = new Supplier
				{

					Name = supplierDto.Name,
					Phone = supplierDto.Phone,
					Email = supplierDto.Email,
					Adress = supplierDto.Adress,
				};

				await _supplierRepository.AddAsync(supplier);
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al crear los suplidores: {ex.Message}");
			}
		}

		public async Task DeleteSupplierAsync(int id)
		{
			try
			{
				await _supplierRepository.DeleteAsync(id);
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al eliminar los suplidores: {ex.Message}");
			}
		}

		public async Task<IEnumerable<SupplierDTO>> GetAllSupplierAsync()
		{
			try
			{
				var suppliers = await _supplierRepository.GetAllAsync();
				if(suppliers == null || !suppliers.Any())
				{
					throw new Exception("No se encontraron los suplidores.");
				}

				return suppliers.Select(p => new SupplierDTO
				{
					SupplierId = p.Id,
					Name=p.Name,
					Phone=p.Phone,
					Email=p.Email,
					Adress=p.Adress,
					
				});

			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al obtener los suplidores: {ex.Message}");
			}
		}

		public async Task<SupplierDTO> GetSupplierByIdAsync(int id)
		{
			try
			{
				var supplier = await _supplierRepository.GetByIdAsync(id);
				if (supplier == null)
				{
					throw new Exception($"supplier con ID {id} no encontrada.");
				}

				return new SupplierDTO
				{
					SupplierId=supplier.Id,
					Name=supplier.Name,
					Phone=supplier.Phone,
					Email=supplier.Email,
					Adress=supplier.Adress,
				};
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al obtener el suplidor: {ex.Message}");
			}
		}

		public async Task UpdateSupplierAsync(int id, UpdateSupplierDTO supplierDto)
		{
			try
			{
				var supplier = await _supplierRepository.GetByIdAsync(id);
				if (supplier == null)
				{
					throw new Exception($"Suplidor con ID {id} no encontrada.");
				}

				supplier.Name = supplierDto.Name;
				supplier.Phone = supplierDto.Phone;
				supplier.Email = supplierDto.Email;
				supplier.Adress = supplierDto.Adress;


				await _supplierRepository.UpdateAsync(supplier);
			}
			catch (Exception ex)
			{

				throw new Exception($"Ocurrió un error al actualizar el suplidor: {ex.Message}");
			}
		}
	}
}
