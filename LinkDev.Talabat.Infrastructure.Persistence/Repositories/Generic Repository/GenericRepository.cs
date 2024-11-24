using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Repositories.Generic_Repository
{
	internal class GenericRepository<TEntity, TKey>(StoreDbContext DbContext) : IGenericRepository<TEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public async Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false)
		{
			//if (typeof(TEntity) == typeof(Product))
			//{

			//	return (IEnumerable<TEntity>) (withTracking? await DbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).ToListAsync() :
			//						  await DbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).AsNoTracking().ToListAsync());

			//}


			return withTracking ? await DbContext.Set<TEntity>().ToListAsync() :
									  await DbContext.Set<TEntity>().AsNoTracking().ToListAsync();

		}

		public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, TKey> spec, bool withTracking = false)
		{
			return await ApplySpecifications(spec).ToListAsync();

			//_dbContext.Set<Product>().Where( P => P.BrandId == 1 && P.CategoryId == 1).OrderBy(P => P.Name).Include(P => P.Brand).Include(P => P.Category).ToListAsync();

			// query = _dbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).ToListAsync();

		}

		/// {
		/// 	if (withTracking) return await DbContext.Set<TEntity>().ToListAsync();
		/// 
		/// 	return await DbContext.Set<TEntity>().AsNoTracking().ToListAsync();
		/// }


		public async Task<TEntity?> GetAsync(TKey id)
		{

			if (typeof(TEntity) == typeof(Product))
				return await DbContext.Set<Product>().Where(P => P.Id.Equals(id)).Include(P => P.Brand).Include(P => P.Category).FirstOrDefaultAsync() as TEntity;

			return await DbContext.Set<TEntity>().FindAsync(id);
		}

		public async Task<TEntity?> GetWithSpecAsync(ISpecifications<TEntity, TKey> spec)
		{
			return await ApplySpecifications(spec).FirstOrDefaultAsync();
			// query = _dbContext.Set<Product>().Where(P => P.Id == 1).Include(P => P.Brand).Include(P => P.Category).FirstOrDefaultAsync();

		}


		public async Task<int> GetCountAsync(ISpecifications<TEntity, TKey> spec)
		{
			return await ApplySpecifications(spec).CountAsync();
		}

		public async Task AddAsync(TEntity entity) => await DbContext.Set<TEntity>().AddAsync(entity);

		public void Delete(TEntity entity) => DbContext.Set<TEntity>().Remove(entity);
		public void Update(TEntity entity) => DbContext.Set<TEntity>().Update(entity);

		#region Helpers
		private IQueryable<TEntity> ApplySpecifications(ISpecifications<TEntity, TKey> spec)
		{
			return SpecificationsEvaluator<TEntity, TKey>.GetQuery(DbContext.Set<TEntity>(), spec);
		}

		
		#endregion

	}
}
