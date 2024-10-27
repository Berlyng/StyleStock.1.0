using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.domain.DTOS
{
	public class CategoryDTO
	{
		[Key]
        public int CategoryId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

    }
}
