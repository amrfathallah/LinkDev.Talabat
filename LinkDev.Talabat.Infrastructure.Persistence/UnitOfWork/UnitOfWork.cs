using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Repositories.Generic_Repository;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
	{
		private readonly StoreDbContext _dbContext;
		private readonly ConcurrentDictionary<string, object> _repositories;
		public UnitOfWork(StoreDbContext dbContext)
        {
			_dbContext = dbContext;
			_repositories = new ();
		}

		public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
			where TEntity : BaseEntity<TKey>
			where TKey : IEquatable<TKey>
		{
			/// var typeName = typeof(TEntity).Name; // Product
			/// if (_repositories.ContainsKey(typeName)) return (IGenericRepository<TEntity, TKey>)_repositories[typeName];
			/// 
			/// var repository = new GenericRepository<TEntity, TKey>(_dbContext);
			/// _repositories.Add(typeName, repository);
			/// 
			/// return repository;

			return (IGenericRepository<TEntity, TKey>) _repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, TKey>(_dbContext));
		}
		public Task<int> CompleteAsync() => _dbContext.SaveChangesAsync();

		public ValueTask DisposeAsync() => _dbContext.DisposeAsync();

		
	}
}
