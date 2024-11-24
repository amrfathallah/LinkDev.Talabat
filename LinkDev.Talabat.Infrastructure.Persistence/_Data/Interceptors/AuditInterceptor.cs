using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Interceptors
{
	public class AuditInterceptor : SaveChangesInterceptor
	{
		private readonly ILoggedInUserService _loggedInUserService;

		public AuditInterceptor(ILoggedInUserService LoggedInUserService)
		{
			_loggedInUserService = LoggedInUserService;
		}

		public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
		{
			UpdateEntities(eventData.Context);
			return base.SavingChanges(eventData, result);
		}

		public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
		{
			UpdateEntities(eventData.Context);
			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}



		private void UpdateEntities(DbContext? dbContext)
		{

			if (dbContext is null) return;

			var entries = dbContext.ChangeTracker.Entries<IBaseAuditableEntity>()
						  .Where(entity => entity.State is EntityState.Added or EntityState.Modified);

			var utcNow = DateTime.UtcNow;

			foreach (var entry in entries)
			{

				// if (entry.Entity is Order or OrderItem)
				// 	_loggedInUserService.UserId = "";

				if (entry.State is EntityState.Added)
				{
					entry.Entity.CreatedBy = _loggedInUserService.UserId!;
					entry.Entity.CreatedOn = utcNow;
				}
				entry.Entity.LastModifiedBy = _loggedInUserService.UserId!;
				entry.Entity.LastModifiedOn = utcNow;

			}
		}
	}
}
