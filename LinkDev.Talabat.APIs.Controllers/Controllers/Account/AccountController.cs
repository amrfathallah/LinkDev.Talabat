using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.Auth.Models;
using LinkDev.Talabat.Core.Application.Abstraction.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Account
{
	public class AccountController(IServiceManager serviceManager) : ApiControllerBase
	{


		[HttpPost("login")] // POST: /api/account/login
		public async Task<ActionResult<UserDto>> Login(LoginDto model)
		{
			var response = await serviceManager.AuthService.LoginAsync(model);
			return Ok(response);
		}

		[HttpPost("register")] // POST: /api/account/register
		public async Task<ActionResult<UserDto>> Register(RegisterDto model)
		{
			var response = await serviceManager.AuthService.RegisterAsync(model);
			return Ok(response);
		}

		[Authorize]
		[HttpGet] // GET: /api/account/getcurredntuser
		public async Task<ActionResult<UserDto>> GetCurrentUser()
		{
			var result = await serviceManager.AuthService.GetCurrentUser(User);

			return Ok(result);
		}


		[Authorize]
		[HttpGet("address")] // GET: /api/account/address

		public async Task<ActionResult<AddressDto>> GetUserAddress()
		{
			var result = await serviceManager.AuthService.GetUserAddress(User);

			return Ok(result);
		}


		[Authorize]
		[HttpPut("address")] // PUT: /api/account/address 
		public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
		{
			var result = await serviceManager.AuthService.UpdateUserAddress(User, address);

			return Ok(result);
		}
	}
}
