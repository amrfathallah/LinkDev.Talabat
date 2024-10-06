
using LinkDev.Talabat.Infrastructure.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.APIs
{
	public class Program
	{
		// Entry Point
		public static async Task Main(string[] args)
		{
			var webApplicationBuilder = WebApplication.CreateBuilder(args);

			#region Configure Services

			// Add services to the container.

			webApplicationBuilder.Services.AddControllers(); // Regester Required Services by ASP.NET Core Web APIs to DI Container.

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			webApplicationBuilder.Services.AddEndpointsApiExplorer();
			webApplicationBuilder.Services.AddSwaggerGen();


			webApplicationBuilder.Services.AddPersistenceServices(webApplicationBuilder.Configuration);

			#endregion

			#region Update Database

			var app = webApplicationBuilder.Build();

			using var scope = app.Services.CreateAsyncScope();
			var services = scope.ServiceProvider;
			var dbContext = services.GetRequiredService<StoreContext>();
			// Ask Runtime Env for an Opject from "StoreContext" Service Explicitly.

			var loggerFactory = services.GetRequiredService<ILoggerFactory>();

			try
			{
				var pendingMigrations = dbContext.Database.GetPendingMigrations();

				if (pendingMigrations.Any())
					await dbContext.Database.MigrateAsync(); // Update-Database
			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "an error has been occured during applying the migrations.");
			} 

			#endregion

			#region Configure Kestrel Middlewares

			// Configure the HTTP request pipeline.

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			

			app.MapControllers(); 

			#endregion

			app.Run();
		}
	}
}
