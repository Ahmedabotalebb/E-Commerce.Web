using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity,TKey> (IQueryable<TEntity> Inputquery ,   ISpecification<TEntity,TKey> specification )where TEntity : BaseEntity<TKey>
        {
            var Query = Inputquery;
            if(specification.Criteria is not null)
            {
                Query = Query.Where(specification.Criteria);
            }

            if(specification.IncludeExpressions is not null && specification.IncludeExpressions.Count > 0)
            {
                Query = specification.IncludeExpressions.Aggregate(Query, (CurrentQuery, IncludeExp) => CurrentQuery.Include(IncludeExp));
            }
            return Query;
        }
    }
}
