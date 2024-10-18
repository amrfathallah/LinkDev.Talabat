using LinkDev.Talabat.Core.Application.Abstraction.Auth.Models;

namespace LinkDev.Talabat.Core.Application.Abstraction.Auth
{
	public interface IAuthService
	{
		Task<UserDto> LoginAsync(LoginDto model);

		Task<UserDto> RegisterAsync(RegisterDto model);
	}
}
