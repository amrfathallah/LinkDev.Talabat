
using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.APIs.Extensions;
using LinkDev.Talabat.APIs.Middlewares;
using LinkDev.Talabat.APIs.Services;
using LinkDev.Talabat.Core.Application;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure;
using LinkDev.Talabat.Infrastructure.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence._Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

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

			webApplicationBuilder.Services
				.AddControllers()
				.ConfigureApiBehaviorOptions(options =>
				{
					options.SuppressModelStateInvalidFilter = false;
					options.InvalidModelStateResponseFactory = (actionContext) =>
					{
						var errors = actionContext.ModelState.Where(P => P.Value!.Errors.Count > 0)
									   .Select(P => new ApiValidationErrorResponse.ValidationError()
									   {
										   Field = P.Key,
										   Errors = P.Value!.Errors.Select(E => E.ErrorMessage)
									   });
						return new BadRequestObjectResult(new ApiValidationErrorResponse()
						{
							Errors = errors
						});
					};
				})
				.AddApplicationPart(typeof(Controllers.AssemblyInformation).Assembly); // Regester Required Services by ASP.NET Core Web APIs to DI Container.

		

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			webApplicationBuilder.Services.AddEndpointsApiExplorer();
			webApplicationBuilder.Services.AddSwaggerGen();

			//webApplicationBuilder.Services.AddScoped(typeof(IHttpContextAccessor), typeof(HttpContextAccessor));
			webApplicationBuilder.Services.AddHttpContextAccessor();
			webApplicationBuilder.Services.AddScoped(typeof(ILoggedInUserService), typeof(LoggedInUserService));



			webApplicationBuilder.Services.AddApplicationServices();
			webApplicationBuilder.Services.AddPersistenceServices(webApplicationBuilder.Configuration);
			webApplicationBuilder.Services.AddInfrastructureServices(webApplicationBuilder.Configuration);

			webApplicationBuilder.Services.AddIdentity<ApplicationUser, IdentityRole>((identityOptions) =>
			{
				identityOptions.SignIn.RequireConfirmedAccount = true;
				identityOptions.SignIn.RequireConfirmedEmail = true;
				identityOptions.SignIn.RequireConfirmedPhoneNumber = true;

				/// identityOptions.Password.RequireNonAlphanumeric = true; // $#@%
				/// identityOptions.Password.RequiredUniqueChars = 2;
				/// identityOptions.Password.RequiredLength = 6;
				/// identityOptions.Password.RequireDigit = true;
				/// identityOptions.Password.RequireLowercase = true;
				/// identityOptions.Password.RequireUppercase = true;

				identityOptions.User.RequireUniqueEmail = true;
				//identityOptions.User.AllowedUserNameCharacters = "abcdenkotlg93124568_-+@#$";

				identityOptions.Lockout.AllowedForNewUsers = true;
				identityOptions.Lockout.MaxFailedAccessAttempts = 5;
				identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(12);

				//identityOptions.Stores
				//identityOptions.Tokens
				//identityOptions.ClaimsIdentity

			})
				.AddEntityFrameworkStores<StoreIdentityDbContext>();

			#endregion

			var app = webApplicationBuilder.Build();

			#region Update Databases Initialization

			await app.InitializeDbAsync();

			#endregion

			#region Configure Kestrel Middlewares

			// Configure the HTTP request pipeline.

			app.UseMiddleware<ExceptionHandlerMiddleware>();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
				
				//app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();


			app.UseStatusCodePagesWithReExecute("/Errors/{0}");

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseStaticFiles();

			app.MapControllers(); 

			#endregion

			app.Run();
		}
	}
}
