using LinkDev.Talabat.Core.Application.Abstraction.Auth.Models;
using LinkDev.Talabat.Core.Application.Abstraction.Common.Models;
using System.Security.Claims;

namespace LinkDev.Talabat.Core.Application.Abstraction.Auth
{
	public interface IAuthService
	{
		Task<UserDto> LoginAsync(LoginDto model);

		Task<UserDto> RegisterAsync(RegisterDto model);

		Task<UserDto> GetCurrentUser(ClaimsPrincipal claimsPrincipal);

		Task<AddressDto> GetUserAddress(ClaimsPrincipal claimsPrincipal);

	}
}
