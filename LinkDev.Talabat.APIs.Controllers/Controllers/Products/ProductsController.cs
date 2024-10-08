using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.Products.Models;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Products
{
	public class ProductsController(IServiceManager serviceManager) : ApiControllerBase
	{


		[HttpGet] // GET: /api/Products
		public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
		{
			var products = await serviceManager.ProductService.GetProductsAsync();
			return Ok(products);
		}

		[HttpGet("{id}")] // GET: /api/Products/id
		public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts(int id)
		{
			var products = await serviceManager.ProductService.GetProductAsync(id);

			if (products == null)
				return NotFound();
			
			return Ok(products);
		}


		[HttpGet("brands")] // GET: /api/Products/brands
		public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
		{
			var brands = await serviceManager.ProductService.GetBrandsAsync();
			return Ok(brands);
		}

		[HttpGet("categories")] // GET: /api/Products/categories
		public async Task<ActionResult<IEnumerable<BrandDto>>> GetCategories()
		{
			var categories = await serviceManager.ProductService.GetCategoriesAsync();
			return Ok(categories);
		}
	}
}
