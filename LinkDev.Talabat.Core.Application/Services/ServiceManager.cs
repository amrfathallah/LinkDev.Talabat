using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Products;
using LinkDev.Talabat.Core.Application.Services.Auth;
using LinkDev.Talabat.Core.Application.Services.Basket;
using LinkDev.Talabat.Core.Application.Services.Products;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Services
{
	internal class ServiceManager : IServiceManager
	{
		private readonly Lazy<IProductService> _productService;
		private readonly Lazy<IBasketService> _basketService;
		private readonly Lazy<IAuthService> _authService;
		private readonly IConfiguration _configuration;

		public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, Func<IBasketService> basketServiceFactory, Func<IAuthService> authServiceFactory)
		{
			
			_configuration = configuration;

			_productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
			_basketService = new Lazy<IBasketService>(basketServiceFactory, LazyThreadSafetyMode.ExecutionAndPublication);
			_authService = new Lazy<IAuthService>(authServiceFactory, LazyThreadSafetyMode.ExecutionAndPublication);
		}

		public IProductService ProductService => _productService.Value;

		public IBasketService BasketService => _basketService.Value;

		public IAuthService AuthService => _authService.Value;
	}
}
