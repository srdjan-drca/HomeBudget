using System.Collections.Generic;
using HomeBudget.DataAccess.Models;

namespace HomeBudget.DataAccess.Repositories.Interfaces {

   public interface ICostRepository {

      bool Save(CostDbModel cost);

      CostDbModel Get(int id);

      List<CostDbModel> GetAll();
   }
}