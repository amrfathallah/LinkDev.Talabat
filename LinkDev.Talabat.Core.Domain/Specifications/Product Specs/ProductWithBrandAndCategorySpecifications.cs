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
		public ProductWithBrandAndCategorySpecifications(string? sort, int? brandId, int? categoryId, int pageSize, int pageIndex, string? search)
			: base(

				  P =>
						(string.IsNullOrEmpty(search) || P.NormalizedName.Contains(search))
							&&
						(!brandId.HasValue || P.BrandId == brandId.Value)
							&&
						(!categoryId.HasValue || P.CategoryId == categoryId.Value)


				  )
		{
			AddIncludes();



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

			// totalProducts = 18 ~ 20
			// pageSize      = 5
			// pageindex     = 3

			ApplyPagination(pageSize * (pageIndex - 1), pageSize);
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
