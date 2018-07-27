using System.Collections.Generic;
using HomeBudget.DataAccess.Models;

namespace HomeBudget.DataAccess.Repositories.Interfaces {

   public interface ICriteriaValueRepository {

      List<CriteriaValueDbModel> GetAllValues(string criteriaCode);

      int GetId(string criteriaCode, string criteriaValueCode);
   }
}