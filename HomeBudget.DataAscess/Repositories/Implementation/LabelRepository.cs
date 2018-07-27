using HomeBudget.DataAccess.Core;
using HomeBudget.DataAccess.Repositories.Interfaces;

namespace HomeBudget.DataAccess.Repositories.Implementation {

   public class LabelRepository : ILabelRepository {
      private ISqlServerDatabase _sqlServerDatabase;

      public LabelRepository(ISqlServerDatabase sqlServerDatabase) {
         _sqlServerDatabase = sqlServerDatabase;
      }
   }
}