using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;

namespace Service.Specification
{
    abstract class BaseSpecification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected BaseSpecification(Expression<Func<TEntity,bool>>? CriteiaExpression)
        {
            Criteria = CriteiaExpression;
        }
        #region Include
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new List<Expression<Func<TEntity, object>>>();


        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);
        }
        #endregion


        #region Sorting
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }
       

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderbyExp) => OrderBy = orderbyExp;
        protected void AddOrderByDecsinding(Expression<Func<TEntity, object>> orderbyDescExp) => OrderByDescending = orderbyDescExp;
        #endregion
        #region Pagenation
        public  int Take { get; private set; }
        public  int Skip { get; private set; }
        public  bool IsPagenated { get; set; }
        protected void ApplyPagenation(int PageSize , int PageIndex)
        {
            IsPagenated = true;
            Take = PageSize;
            Skip =(PageIndex-1)*PageSize;
        }
        #endregion
    }
}
