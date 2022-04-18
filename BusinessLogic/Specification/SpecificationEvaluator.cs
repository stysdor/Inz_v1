﻿using BusinessLogic.BaseSpecification;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Specification
{
    public class SpecificationEvaluator<T>
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var query = inputQuery;

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            if (spec.IsPagingEnabled && spec.Skip.HasValue && spec.Take.HasValue)
            {
                query = query.Skip((int)spec.Skip).Take((int)spec.Take);
            }

            return query;
        }
    }
}
