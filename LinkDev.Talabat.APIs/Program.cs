
using LinkDev.Talabat.Infrastructure.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.APIs
{
	public class Program
	{
		// Entry Point
		public static void Main(string[] args)
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

			var app = webApplicationBuilder.Build();

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
