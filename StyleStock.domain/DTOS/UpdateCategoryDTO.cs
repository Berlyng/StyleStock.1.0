﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.domain.DTOS
{
	public class UpdateCategoryDTO:CategoryDTO
	{
        public int CategoryId { get; set; }
    }
}
