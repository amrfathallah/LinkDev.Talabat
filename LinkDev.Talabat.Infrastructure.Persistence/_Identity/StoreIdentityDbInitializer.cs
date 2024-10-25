using LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbInitializers;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence._Common;
using Microsoft.AspNetCore.Identity;

namespace LinkDev.Talabat.Infrastructure.Persistence._Identity
{
	internal sealed class StoreIdentityDbInitializer(StoreIdentityDbContext _dbContext, UserManager<ApplicationUser> _userManager) : DbInitializer(_dbContext), IStoreIdentityDbInitializer
	{
		
		public override async Task SeedAsync()
		{
			var user = new ApplicationUser()
			{
				DisplayName = "Amr Mohamed",
				UserName = "amr.mohamed",
				Email = "amr.mohamed@linkdev.com",
				PhoneNumber = "01234567890"
			};

			await _userManager.CreateAsync(user, "P@ssw0rd");
		}
	}
}
