using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Repositories.Generic_Repository
{
	internal static class SpecificationsEvaluator<TEntity, TKey>
		where TEntity : BaseAuditableEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> spec)
		{
			var query = inputQuery; // _dbContext.Set<Product>()

			if(spec.Criteria is not null) // P => P.Id == 1
				query = query.Where(spec.Criteria);

			// query = _dbContext.Set<Product>().Where(P => P.Id == 1);
			// include expressions
			// 1. P => P.Brand
			// 2. P => P.Category
			// ...

			query = spec.Includes.Aggregate(query, (currentQuery, include) => currentQuery.Include(include));

			// query = _dbContext.Set<Product>().Where(P => P.Id == 1).Include(P => P.Brand);
			// query = _dbContext.Set<Product>().Where(P => P.Id == 1).Include(P => P.Brand).Include(P => P.Category);



			return query;
		}
	}
}
