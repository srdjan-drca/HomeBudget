using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using HomeBudget.DataAccess.Core;
using HomeBudget.DataAccess.Models;
using HomeBudget.DataAccess.Repositories.Interfaces;
using HomeBudget.Tools.Helpers;

namespace HomeBudget.DataAccess.Repositories.Implementation {

   public class CriteriaValueRepository : ICriteriaValueRepository {
      private readonly ISqlServerDatabase _sqlServerDatabase;

      private readonly ICriteriaRepository _criteriaRepository;

      public CriteriaValueRepository(ISqlServerDatabase sqlServerDatabase, ICriteriaRepository criteriaRepository) {
         _sqlServerDatabase = sqlServerDatabase;
         _criteriaRepository = criteriaRepository;
      }

      public List<CriteriaValueDbModel> GetAllValues(string criteriaCode) {
         var criteriaValues = new List<CriteriaValueDbModel>();
         int idCriteria = _criteriaRepository.GetId(criteriaCode);
         var sqlCommand = new SqlCommand();

         string sqlQuery = @"
					SELECT * FROM CriteriaValue WHERE RefCriteria = @IdCriteria
			";

         sqlCommand.CommandText = sqlQuery;
         sqlCommand.Parameters.AddWithValue("@IdCriteria", idCriteria);

         IEnumerable<CustomDataRow> dataRowCollection = _sqlServerDatabase.GetCustomDataRowCollection(sqlCommand);

         foreach (var dataRow in dataRowCollection) {
            var criteriaValue = new CriteriaValueDbModel();

            criteriaValue.Id = ConversionHelper.ToInt(dataRow["IdCriteriaValue"]);
            criteriaValue.CodeCriteriaValue = ConversionHelper.ToString(dataRow["CodeCriteriaValue"]);
            criteriaValue.IsActive = ConversionHelper.ToBoolean(dataRow["IsActive"]);
            criteriaValue.SequenceOrder = ConversionHelper.ToInt(dataRow["SequenceOrder"]);
            criteriaValue.RefCriteria = ConversionHelper.ToInt(dataRow["RefCriteria"]);
            criteriaValue.RefLabelCriteriaValueName = ConversionHelper.ToInt(dataRow["RefLabelCriteriaValueName"]);
            criteriaValue.DateTimeCreatedOn = ConversionHelper.ToDateTime(dataRow["DateTimeCreatedOn"]);

            criteriaValues.Add(criteriaValue);
         }

         return criteriaValues;
      }

      public int GetId(string criteriaCode, string criteriaValueCode) {
         List<CriteriaValueDbModel> criteriaValues = GetAllValues(criteriaCode);
         CriteriaValueDbModel criteriaValue = criteriaValues.FirstOrDefault(x => x.CodeCriteriaValue == criteriaValueCode);

         return criteriaValue != null ? criteriaValue.Id : 0;
      }
   }
}