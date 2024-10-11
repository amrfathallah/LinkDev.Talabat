using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Domain.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Interceptors
{
	public class BaseAuditableEntityInterceptor : SaveChangesInterceptor
	{
		private readonly ILoggedInUserService _loggedInUserService;

		public BaseAuditableEntityInterceptor(ILoggedInUserService LoggedInUserService)
        {
			_loggedInUserService = LoggedInUserService;
		}

		public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
		{
			UpdateEntities(eventData.Context);
			return base.SavingChanges(eventData, result);
		}

		public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
		{
			UpdateEntities(eventData.Context);
			return base.SavedChangesAsync(eventData, result, cancellationToken);
		}

		private void UpdateEntities(DbContext? dbContext)
		{
			if (dbContext is null) return;

			var utcNow = DateTime.UtcNow;
			foreach (var entry in dbContext.ChangeTracker.Entries<BaseAuditableEntity<int>>())
			{
				if (entry is { State: EntityState.Added or EntityState.Modified })
				{
					if (entry.State == EntityState.Added)
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
}
