using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Products;
using LinkDev.Talabat.Core.Application.Abstraction.Products.Models;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Core.Domain.Specifications;
using LinkDev.Talabat.Core.Domain.Specifications.Product_Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Services.Products
{
    internal class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {

        public async Task<IEnumerable<ProductToReturnDto>> GetProductsAsync(string? sort)
        {
            var specs = new ProductWithBrandAndCategorySpecifications(sort);

            var products = await unitOfWork.GetRepository<Product, int>().GetAllWithSpecAsync(specs);


			var mappedproducts = mapper.Map<IEnumerable<ProductToReturnDto>>(products);
        
            return mappedproducts;
        }


        public async Task<ProductToReturnDto> GetProductAsync(int id)
        {
			var specs = new ProductWithBrandAndCategorySpecifications(id);

			var product = await unitOfWork.GetRepository<Product, int>().GetWithSpecAsync(specs);


			var mappedproduct = mapper.Map<ProductToReturnDto>(product);

            return mappedproduct;

		}
        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
            => mapper.Map<IEnumerable<BrandDto>>(await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync());


        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
            => mapper.Map<IEnumerable<CategoryDto>>(await unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync());

    }
}
