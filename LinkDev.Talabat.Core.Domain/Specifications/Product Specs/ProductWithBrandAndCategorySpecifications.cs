using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specifications.Product_Specs
{
	public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>
	{
		// This Object is Created via this Constructor, will be Used for Building the Query that Get All Products
        public ProductWithBrandAndCategorySpecifications(string? sort)
            : base()
		{
			AddIncludes();

			AddOrderBy(P => P.Name); // Default

			if (!string.IsNullOrEmpty(sort))
			{
				switch (sort)
				{
					case "nameDesc":
						AddOrderBy(P => P.Name);
						break;
					case "priceAsc":
						AddOrderBy(P => P.Price); 
						break;
					case "priceDesc":
						AddOrderByDesc(P => P.Price); 
						break;
					default:
						AddOrderBy(P => P.Name);
						break;
				}
			}
		}

		

		// This Object is Created via this Constructor, will be Used for Building the Query that Get a Specific Product

		public ProductWithBrandAndCategorySpecifications(int Id)
			: base(Id)
		{
			AddIncludes();
		}


		private protected override void AddIncludes()
		{
			base.AddIncludes();

			Includes.Add(P => P.Brand!);
			Includes.Add(P => P.Category!);
		}
	}
}
