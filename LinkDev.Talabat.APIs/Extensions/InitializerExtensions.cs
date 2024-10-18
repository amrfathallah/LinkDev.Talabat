using LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbInitializers;

namespace LinkDev.Talabat.APIs.Extensions
{
    public static class InitializerExtensions
	{
		public static async Task<WebApplication> InitializeDbAsync(this WebApplication app)
		{

			using var scope = app.Services.CreateAsyncScope();
			var services = scope.ServiceProvider;
			var storeContextInitializer = services.GetRequiredService<IStoreDbInitializer>();
			var storeIdentityContextInitializer = services.GetRequiredService<IStoreIdentityDbInitializer>();


			var loggerFactory = services.GetRequiredService<ILoggerFactory>();

			try
			{
				await storeContextInitializer.InitializeAsync();
				await storeContextInitializer.SeedAsync();

				await storeIdentityContextInitializer.InitializeAsync();
				await storeIdentityContextInitializer.SeedAsync();

			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "an error has been occured during applying the migrations or the Data Seeding.");
			}
			return app;
		}

		
	}
}
