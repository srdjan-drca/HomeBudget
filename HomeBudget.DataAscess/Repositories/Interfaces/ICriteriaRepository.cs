using HomeBudget.DataAccess.Models;

namespace HomeBudget.DataAccess.Repositories.Interfaces {

   public interface ICriteriaRepository {

      CriteriaDbModel Get(string criteriaCode);

      int GetId(string criteriaCode);
   }
}