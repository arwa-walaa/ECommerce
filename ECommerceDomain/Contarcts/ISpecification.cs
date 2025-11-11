using ECommerceDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDomain.Contarcts
{
    public interface ISpecification<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        public ICollection<Expression<Func<TEntity,object> >> IncludeExpression { get; }
        public Expression<Func<TEntity,bool>> Criteria { get; }

    }
}
