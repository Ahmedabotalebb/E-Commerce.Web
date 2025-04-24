using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository
{
    public class GenericRepository<TEntity, Tkey>(StoreDbcontext _Dbcontext) : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _Dbcontext.Set<TEntity>().ToListAsync();
        public async Task<TEntity?> GetByIdAsync(Tkey id) => await _Dbcontext.Set<TEntity>().FindAsync(id);

        public async Task AddAsync(TEntity entity)=>await _Dbcontext.Set<TEntity>().AddAsync(entity);

        public  void Remove(TEntity entity)=> _Dbcontext.Set<TEntity>().Remove(entity);


       

        public void Update(TEntity entity)=>_Dbcontext.Set<TEntity>().Update(entity);
       
    }
}
