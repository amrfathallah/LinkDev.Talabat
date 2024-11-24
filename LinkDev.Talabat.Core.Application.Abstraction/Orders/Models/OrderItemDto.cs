﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Orders.Models
{
	public class OrderItemDto
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public required string ProductName { get; set; }
		public required string PictureUrl { get; set; }
		public decimal Price { get; set; }
		public int Quatity { get; set; }
	}
}
