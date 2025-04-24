using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;

namespace Persistence.Repository
{
    public class UnitOfWork(StoreDbcontext _Dbcontext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            var typename= typeof(TEntity).Name;
            if (_repositories.TryGetValue(typename, out object? value))
            {
                return (IGenericRepository<TEntity, Tkey>)value;
            }
            else
            {
                var repo= new GenericRepository<TEntity,Tkey>(_Dbcontext);
                _repositories["typename"] = repo;
                return repo;
            }
        }

        public async Task<int> SavechangesAsync()
        {
            return  await _Dbcontext.SaveChangesAsync();
        }
    }
}
