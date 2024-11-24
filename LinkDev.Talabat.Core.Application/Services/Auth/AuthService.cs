﻿using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Auth.Models;
using LinkDev.Talabat.Core.Application.Abstraction.Common.Models;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Application.Extensions;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LinkDev.Talabat.Core.Application.Services.Auth
{
	public class AuthService(
		IMapper mapper,
		IOptions<JwtSettings> jwtSettings,
		UserManager<ApplicationUser> userManager,
		SignInManager<ApplicationUser> signInManager) : IAuthService
	{


		private readonly JwtSettings _jwtSettings = jwtSettings.Value;

		public async Task<UserDto> GetCurrentUser(ClaimsPrincipal claimsPrincipal)
		{
			var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);

			var user = await userManager.FindByEmailAsync(email!);

			return new UserDto()
			{
				Id = user!.Id,
				Email = user!.Email!,
				DisplayName = user.DisplayName,
				Token = await GenerateTokenAsync(user)

			};
		}

		public async Task<AddressDto?> GetUserAddress(ClaimsPrincipal claimsPrincipal)
		{

			var user = await userManager.FindUserWithAddress(claimsPrincipal!);

			var address = mapper.Map<AddressDto>(user!.Address);

			return address;
		}

		public async Task<AddressDto> UpdateUserAddress(ClaimsPrincipal claimsPrincipal, AddressDto addressDto)
		{
			var updatedAddress = mapper.Map<Address>(addressDto);

			var user = await userManager.FindUserWithAddress(claimsPrincipal);

			if(user?.Address is not null)
				updatedAddress.Id = user.Address.Id;

			user!.Address = updatedAddress;

			var result = await userManager.UpdateAsync(user);

			if (!result.Succeeded) throw new BadRequestException(result.Errors.Select(error => error.Description).Aggregate((X, Y) => $"{X}, {Y}"));

			return addressDto;
		}

		public async Task<UserDto> LoginAsync(LoginDto model)
		{
			var user = await userManager.FindByEmailAsync(model.Email);

			if (user is null) throw new UnAuthorizedException("Invalid Login");

			var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure : true);

			if (result.IsNotAllowed) throw new UnAuthorizedException("Account not confirmed yet.");

			if (result.IsLockedOut) throw new UnAuthorizedException("Account is Locked.");

			//if (result.RequiresTwoFactor) throw new UnAuthorizedException("Requires Two-Factor Authentication");

			if (!result.Succeeded) throw new UnAuthorizedException("Invalid Login");

			var response = new UserDto()
			{
				Id = user.Id,
				DisplayName = user.DisplayName,
				Email = user.Email!,
				Token = await GenerateTokenAsync(user)

			};

			return response;

		}

		public async Task<UserDto> RegisterAsync(RegisterDto model)
		{
			var user = new ApplicationUser()
			{
				DisplayName = model.DisplayName,
				Email = model.Email,
				UserName = model.UserName,
				PhoneNumber = model.Phone
			};

			var result = await userManager.CreateAsync(user , model.Password);

			if (!result.Succeeded) throw new ValidationException() { Errors = result.Errors.Select(E => E.Description)};

			var response = new UserDto()
			{
				Id = user.Id,
				DisplayName = user.DisplayName,
				Email = user.Email!,
				Token = await GenerateTokenAsync(user)

			};

			return response;

		}

		

		private async Task<string> GenerateTokenAsync(ApplicationUser user)
		{

			// Private claims
			var PrivateClaims = new List<Claim>()
			{
				new Claim(ClaimTypes.PrimarySid, user.Id),
				new Claim(ClaimTypes.Email, user.Email!),
				new Claim(ClaimTypes.GivenName, user.DisplayName),
			}
			.Union(await userManager.GetClaimsAsync(user)).ToList();

			var roles = await userManager.GetRolesAsync(user);

			foreach (var role  in roles)
				PrivateClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));

			var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

			var tokenObj = new JwtSecurityToken(


				audience: _jwtSettings.Audience,
				issuer:_jwtSettings.Issuer,
				expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
				claims: PrivateClaims,
				signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)

				);

			return new JwtSecurityTokenHandler().WriteToken(tokenObj);

		}
	}
}
