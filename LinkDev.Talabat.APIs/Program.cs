
using LinkDev.Talabat.APIs.Extensions;
using LinkDev.Talabat.APIs.Services;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application;
using LinkDev.Talabat.Core.Domain.Contracts;
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

			webApplicationBuilder.Services.AddControllers().AddApplicationPart(typeof(Controllers.AssemblyInformation).Assembly); // Regester Required Services by ASP.NET Core Web APIs to DI Container.

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			webApplicationBuilder.Services.AddEndpointsApiExplorer();
			webApplicationBuilder.Services.AddSwaggerGen();

			//webApplicationBuilder.Services.AddScoped(typeof(IHttpContextAccessor), typeof(HttpContextAccessor));
			webApplicationBuilder.Services.AddScoped(typeof(ILoggedInUserService), typeof(LoggedInUserService));



			webApplicationBuilder.Services.AddPersistenceServices(webApplicationBuilder.Configuration);

			webApplicationBuilder.Services.AddApplicationServices();

			#endregion

			var app = webApplicationBuilder.Build();

			#region Update Databases Initialization

			await app.InitializeStoreContextAsync();

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
