using System.Data.SqlClient;
using HomeBudget.DataAccess.Core;
using HomeBudget.DataAccess.Models;
using HomeBudget.DataAccess.Repositories.Interfaces;
using HomeBudget.Tools.Helpers;

namespace HomeBudget.DataAccess.Repositories.Implementation {

   public class CriteriaRepository : ICriteriaRepository {
      private readonly ISqlServerDatabase _sqlServerDatabase;

      public CriteriaRepository(ISqlServerDatabase sqlServerDatabase) {
         _sqlServerDatabase = sqlServerDatabase;
      }

      public CriteriaDbModel Get(string criteriaCode) {
         var sqlCommand = new SqlCommand();

         string sqlQuery = @"
					SELECT * FROM Criteria WHERE CodeCriteria = @CodeCriteria
			";

         sqlCommand.CommandText = sqlQuery;
         sqlCommand.Parameters.AddWithValue("@CodeCriteria", criteriaCode);

         CustomDataRow dataRow = _sqlServerDatabase.GetCustomDataRow(sqlCommand);

         var criteria = new CriteriaDbModel();
         criteria.Id = ConversionHelper.ToInt(dataRow["IdCriteria"]);
         criteria.CodeCriteria = ConversionHelper.ToString(dataRow["CodeCriteria"]);
         criteria.IsActive = ConversionHelper.ToBoolean(dataRow["IsActive"]);
         criteria.SequenceOrder = ConversionHelper.ToInt(dataRow["SequenceOrder"]);
         criteria.RefLabelCriteriaName = ConversionHelper.ToInt(dataRow["RefLabelCriteriaName"]);
         criteria.DateTimeCreatedOn = ConversionHelper.ToDateTime(dataRow["DateTimeCreatedOn"]);

         return criteria;
      }

      public int GetId(string criteriaCode) {
         CriteriaDbModel criteria = Get(criteriaCode);

         return criteria != null ? criteria.Id : 0;
      }
   }
}