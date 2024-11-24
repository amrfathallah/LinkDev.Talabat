using LinkDev.Talabat.Core.Application.Abstraction.Auth.Models;
using System.Security.Claims;

namespace LinkDev.Talabat.Core.Application.Abstraction.Auth
{
	public interface IAuthService
	{
		Task<UserDto> LoginAsync(LoginDto model);

		Task<UserDto> RegisterAsync(RegisterDto model);

		Task<UserDto> GetCurrentUser(ClaimsPrincipal claimsPrincipal);
	}
}
