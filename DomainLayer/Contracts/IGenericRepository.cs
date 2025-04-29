using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace DomainLayer.Contracts
{
    public interface IGenericRepository<TEntity,Tkey> where TEntity : BaseEntity<Tkey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(Tkey id);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        #region Specification

        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, Tkey> specification);
        Task<TEntity?> GetByIdAsync(ISpecification<TEntity, Tkey> specification);
        Task<int> CountAsync(ISpecification<TEntity, Tkey> specification);
        #endregion
    }
}
