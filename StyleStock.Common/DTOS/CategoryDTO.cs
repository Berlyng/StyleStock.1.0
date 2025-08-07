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
        public int Id { get; set; }
		[Required]
        public string Name { get; set; }
		[Required]
		public string Description { get; set; }
    }
}
