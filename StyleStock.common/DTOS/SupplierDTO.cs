﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.common.DTOS
{
	public class SupplierDTO
	{
		public int SupplierId { get; set; }

		public string Name { get; set; } = null!;

		public string Phone { get; set; } = null!;

		public string Email { get; set; } = null!;

		public string Adress { get; set; } = null!;
	}
}