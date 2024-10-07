using LinkDev.Talabat.Core.Domain.Contracts.Persistence;

namespace LinkDev.Talabat.APIs.Extensions
{
    public static class InitializerExtensions
	{
		public static async Task<WebApplication> InitializeStoreContextAsync(this WebApplication app)
		{

			using var scope = app.Services.CreateAsyncScope();
			var services = scope.ServiceProvider;
			var storeContextInitializer = services.GetRequiredService<IStoreContextInitializer>();
			// Ask Runtime Env for an Opject from "StoreContext" Service Explicitly.

			var loggerFactory = services.GetRequiredService<ILoggerFactory>();

			try
			{
				await storeContextInitializer.InitializeAsync();
				await storeContextInitializer.SeedAsync();

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
