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

		public BaseSpecifications()
        {
			//Criteria = null;
        }

		public BaseSpecifications(TKey id)
		{
			Criteria = E => E.Id.Equals(id); // P => P.Id == 1
		}

	}
}
