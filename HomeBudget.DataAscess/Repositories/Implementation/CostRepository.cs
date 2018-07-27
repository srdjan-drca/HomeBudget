using System.Collections.Generic;
using System.Data.SqlClient;
using HomeBudget.DataAccess.Core;
using HomeBudget.DataAccess.Models;
using HomeBudget.DataAccess.Repositories.Interfaces;
using HomeBudget.Tools.Helpers;

namespace HomeBudget.DataAccess.Repositories.Implementation {

   public class CostRepository : ICostRepository {
      private readonly ISqlServerDatabase _sqlServerDatabase;

      public CostRepository(ISqlServerDatabase sqlServerDatabase) {
         _sqlServerDatabase = sqlServerDatabase;
      }

      public bool Save(CostDbModel cost) {
         var sqlCommand = new SqlCommand();

         string sqlQuery = @"
					INSERT INTO Cost (IsActive, RefCriteriaValueCostType, Amount, DateTimeCreatedOn)
					VALUES (@IsActive, @RefCriteriaValueCostType, @Amount, @DateTimeCreatedOn)
			";

         sqlCommand.CommandText = sqlQuery;
         sqlCommand.Parameters.AddWithValue("@IsActive", 1);
         sqlCommand.Parameters.AddWithValue("@RefCriteriaValueCostType", cost.RefCriteriaValueCostType);
         sqlCommand.Parameters.AddWithValue("@Amount", cost.Amount);
         sqlCommand.Parameters.AddWithValue("@DateTimeCreatedOn", cost.DateTimeCreatedOn);

         _sqlServerDatabase.ExecuteNonQuery(sqlCommand);

         return false;
      }

      public CostDbModel Get(int id) {
         var sqlCommand = new SqlCommand();

         string sqlQuery = @"
					SELECT * FROM Cost WHERE IdCost = @IdCost
			";

         sqlCommand.CommandText = sqlQuery;
         sqlCommand.Parameters.AddWithValue("@IdCost", id);

         CustomDataRow dataRow = _sqlServerDatabase.GetCustomDataRow(sqlCommand);

         var cost = new CostDbModel();
         cost.Id = ConversionHelper.ToInt(dataRow["IdCost"]);
         cost.IsActive = ConversionHelper.ToBoolean(dataRow["IsActive"]);
         cost.RefCriteriaValueCostType = ConversionHelper.ToInt(dataRow["RefCriteriaValueCostType"]);
         cost.Amount = ConversionHelper.ToFloat(dataRow["Amount"]);
         cost.DateTimeCreatedOn = ConversionHelper.ToDateTime(dataRow["DateTimeCreatedOn"]);

         return cost;
      }

      public List<CostDbModel> GetAll() {
         var costs = new List<CostDbModel>();
         var sqlCommand = new SqlCommand();

         string sqlQuery = @"
					SELECT * FROM Cost
			";

         sqlCommand.CommandText = sqlQuery;

         IEnumerable<CustomDataRow> dataRowCollection = _sqlServerDatabase.GetCustomDataRowCollection(sqlCommand);

         foreach (var dataRow in dataRowCollection) {
            var cost = new CostDbModel();

            cost.Id = ConversionHelper.ToInt(dataRow["IdCost"]);
            cost.IsActive = ConversionHelper.ToBoolean(dataRow["IsActive"]);
            cost.RefCriteriaValueCostType = ConversionHelper.ToInt(dataRow["RefCriteriaValueCostType"]);
            cost.Amount = ConversionHelper.ToFloat(dataRow["Amount"]);
            cost.DateTimeCreatedOn = ConversionHelper.ToDateTime(dataRow["DateTimeCreatedOn"]);

            costs.Add(cost);
         }

         return costs;
      }
   }
}