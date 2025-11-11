using ECommerceDomain.Contarcts;
using ECommerceDomain.Entities;
using ECommercePersistence.Data.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercePersistence.Repositires
{
    public class GenaricRepo<TEntity, TKey> : IGenaricRepo<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _dbContext;

        public GenaricRepo(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(TEntity entity)
            => await _dbContext.Set<TEntity>().AddAsync(entity);


        public void Delete(TEntity entity) =>
            _dbContext.Set<TEntity>().Remove(entity);
        

        public async Task<IEnumerable<TEntity>> GetAllAsync() => 
            await _dbContext.Set<TEntity>().ToListAsync();

      

        public async Task<TEntity?> GetByIdAsync(TKey id) =>
            await _dbContext.Set<TEntity>().FindAsync(id);


        public void Update(TEntity entity)=>
            _dbContext.Set<TEntity>().Update(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, TKey> specification)
        {
           var query = SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specification);
              return await query.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(ISpecification<TEntity, TKey> specification)
        {
            var query = SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specification);
            return await query.FirstOrDefaultAsync();

        }

        public async Task<int> CountAsync(ISpecification<TEntity, TKey> specification)
        {
            var query = SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specification);
            return await query.CountAsync();

        }
    }
}
