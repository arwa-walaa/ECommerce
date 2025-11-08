using ECommerceDomain.Contarcts;
using ECommerceDomain.Entities;
using ECommercePersistence.Data.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercePersistence.Repositires
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;
        private readonly Dictionary<Type,object> _repositories = [];

        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IGenaricRepo<TEntity, TKey> GetRepo<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var EntityType = typeof(TEntity);
            if(_repositories.TryGetValue(EntityType,out var repository))
            {
                return (IGenaricRepo<TEntity,TKey>) repository;
            }
            var NewRepo = new GenaricRepo<TEntity, TKey>(_dbContext);
            _repositories[EntityType] = NewRepo;
            return NewRepo;

           
        }

        public async Task<int> SaveChangesAsync()=>
           await _dbContext.SaveChangesAsync();
        
    }
}
