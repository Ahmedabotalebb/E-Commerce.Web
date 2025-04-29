using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace DomainLayer.Contracts
{
    public interface ISpecification<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; }
        public List<Expression<Func<TEntity, Object>>>? IncludeExpressions { get; }
        public Expression<Func<TEntity,Object>> OrderBy { get; }
        public Expression<Func<TEntity,Object>> OrderByDescending { get; }
        public int Take { get; }
        public int Skip { get; }
        public bool IsPagenated { get; set; }

    }
}
