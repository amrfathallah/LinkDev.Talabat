using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LinkDev.Talabat.Core.Domain.Entities.Orders
{
	public class OrderItem : BaseAuditableEntity<int>
	{

		public OrderItem()
		{
		}
		public OrderItem(ProductItemOrdered product, decimal price, int quatity)
		{
			Product = product;
			Price = price;
			Quatity = quatity;
		}


		public required ProductItemOrdered Product { get; set; }
		public decimal Price { get; set; }
		public int Quatity { get; set; }
	}
}