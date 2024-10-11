﻿using LinkDev.Talabat.Core.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specifications
{
	public class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey>
		where TEntity : BaseAuditableEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public Expression<Func<TEntity, bool>>? Criteria {  get; set; } = null;
		public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new ();
		public Expression<Func<TEntity, object>>? OrderBy { get; set; } = null;
		public Expression<Func<TEntity, object>>? OrderByDesc { get; set; } = null;

		protected BaseSpecifications(Expression<Func<TEntity, bool>> criteriaExpression)
        {
			Criteria = criteriaExpression; // P => P.BrandId == 1 && P.CategoryId == 1
		}

		protected BaseSpecifications(TKey id)
		{
			Criteria = E => E.Id.Equals(id); 
		}

		private protected virtual void AddIncludes()
		{
			
		}
		private protected virtual void AddOrderBy(Expression<Func<TEntity, object>> orederByExpression)
		{
			OrderBy = orederByExpression;
		}
		private protected virtual void AddOrderByDesc(Expression<Func<TEntity, object>> orederByExpressionDesc)
		{
			OrderByDesc = orederByExpressionDesc;
		}

	}
}
