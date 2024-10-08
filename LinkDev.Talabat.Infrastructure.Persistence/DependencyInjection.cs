using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Interceptors;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Infrastructure.Persistence
{
    public static class DependencyInjection
	{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection services , IConfiguration configuration)
		{
			services.AddDbContext<StoreContext>((optionsBuilder) =>
			{
				optionsBuilder.UseSqlServer(configuration.GetConnectionString("StoreContext"));
			});


			services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));


			//services.AddScoped<IStoreContextInitializer, StoreContextInitializer>();
			services.AddScoped(typeof(IStoreContextInitializer), typeof(StoreContextInitializer));


			services.AddScoped(typeof(ISaveChangesInterceptor), typeof(BaseAuditableEntityInterceptor));

			return services;
		}
	}
}
