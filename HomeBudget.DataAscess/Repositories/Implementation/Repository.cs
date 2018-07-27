using HomeBudget.DataAccess.Core;
using HomeBudget.DataAccess.Repositories.Interfaces;

namespace HomeBudget.DataAccess.Repositories.Implementation {
   public class Repository<T> : IRepository<T> where T : class, IEntity {
      private readonly ISqlServerDatabase _sqlServerDatabase;

      public Repository(ISqlServerDatabase sqlServerDatabase) {
         _sqlServerDatabase = sqlServerDatabase;
      }

      #region IRepository<T> Members

      public void Insert(T entity) {
         DataTable.InsertOnSubmit(entity);
      }

      //public void Delete(T entity) {
      //   DataTable.DeleteOnSubmit(entity);
      //}

      //public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate) {
      //   return DataTable.Where(predicate);
      //}

      //public IQueryable<T> GetAll() {
      //   return DataTable;
      //}

      //public T GetById(int id) {
      //   // Sidenote: the == operator throws NotSupported Exception!
      //   // 'The Mapping of Interface Member is not supported'
      //   // Use .Equals() instead
      //   return DataTable.Single(e => e.ID.Equals(id));
      //}

      #endregion
   }
}