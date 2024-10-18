using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.Common;
using LinkDev.Talabat.Core.Application.Abstraction.Products.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Products
{
	public class ProductsController(IServiceManager serviceManager) : ApiControllerBase
	{

		[Authorize]
		[HttpGet] // GET: /api/Products
		public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams specParams)
		{
			var products = await serviceManager.ProductService.GetProductsAsync(specParams);
			
			return Ok(products);
		}

		[HttpGet("{id:int}")] // GET: /api/Products/id
		public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProduct(int id)
		{
			var product = await serviceManager.ProductService.GetProductAsync(id);
			
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
