﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Products.Models
{
	public class ProductToReturnDto
	{
        public int Id { get; set; }
        public required string Name { get; set; }
		public required string Description { get; set; }
		public string? PictureUrl { get; set; }
		public decimal Price { get; set; }

		public int? BrandId { get; set; } // Forign Key --> ProductBrand Entity
		public string? Brand { get; set; }

		public int? CategoryId { get; set; } // Forign Key --> ProductCategory Entity
		public string? Category { get; set; }
	}
}
