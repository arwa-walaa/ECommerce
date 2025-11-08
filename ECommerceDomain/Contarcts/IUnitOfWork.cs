using ECommerceDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDomain.Contarcts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        IGenaricRepo<TEntity, TKey> GetRepo<TEntity, TKey>() where TEntity:BaseEntity<TKey> ;


    }
}
