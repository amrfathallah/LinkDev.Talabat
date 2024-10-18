﻿using LinkDev.Talabat.Core.Application.Abstraction.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Auth.Models;
using LinkDev.Talabat.Core.Application.Services.Auth;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence._Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;

namespace LinkDev.Talabat.APIs.Extensions
{
	public static class IdentityExtensions
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection services , IConfiguration configuration)
		{
			services.Configure<JwtSettings>(configuration.GetSection("JWTSettings"));

			services.AddIdentity<ApplicationUser, IdentityRole>((identityOptions) =>
			{
				/// identityOptions.SignIn.RequireConfirmedAccount = true;
				/// identityOptions.SignIn.RequireConfirmedEmail = true;
				/// identityOptions.SignIn.RequireConfirmedPhoneNumber = true;

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

			services.AddScoped(typeof(IAuthService), typeof(AuthService));

			services.AddScoped(typeof(Func<IAuthService>), (serviceProvider) =>
			{
				return () => serviceProvider.GetRequiredService<IAuthService>();
			});

			return services;
		}
	}
}
