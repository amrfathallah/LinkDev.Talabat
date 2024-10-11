using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.Products.Models;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Products
{
	public class ProductsController(IServiceManager serviceManager) : ApiControllerBase
	{


		[HttpGet] // GET: /api/Products
		public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts(string? sort, int? brandId, int? categoryId)
		{
			var products = await serviceManager.ProductService.GetProductsAsync(sort, brandId, categoryId);
			
			return Ok(products);
		}

		[HttpGet("{id:int}")] // GET: /api/Products/id
		public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProduct(int id)
		{
			var product = await serviceManager.ProductService.GetProductAsync(id);

			if (product == null)
				return NotFound(new {StatusCode = 404, message = "not Found."});
			
			return Ok(product);
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
