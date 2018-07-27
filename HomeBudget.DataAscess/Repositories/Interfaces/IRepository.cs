using System;
using System.Linq;
using System.Linq.Expressions;

namespace HomeBudget.DataAccess.Repositories.Interfaces {

   public interface IRepository<TEntity> {

      void Add(TEntity entity);

      void Remove(TEntity entity);

      IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
   }
}