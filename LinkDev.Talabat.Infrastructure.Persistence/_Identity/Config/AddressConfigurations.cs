using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence._Common;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence._Identity.Config
{
	[DbContextType(typeof(StoreIdentityDbContext))]

	internal class AddressConfigurations : IEntityTypeConfiguration<Address>
	{

		public void Configure(EntityTypeBuilder<Address> builder)
		{


			builder.ToTable("Addresses");
		}
	}
}
