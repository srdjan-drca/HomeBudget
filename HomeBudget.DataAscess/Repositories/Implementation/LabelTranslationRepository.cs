using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using HomeBudget.DataAccess.Core;
using HomeBudget.DataAccess.Models;
using HomeBudget.DataAccess.Repositories.Interfaces;
using HomeBudget.Tools.Helpers;

namespace HomeBudget.DataAccess.Repositories.Implementation {

   public class LabelTranslationRepository : ILabelTranslationRepository {
      private readonly ISqlServerDatabase _sqlServerDatabase;

      private readonly ICriteriaValueRepository _criteriaValueRepository;

      public LabelTranslationRepository(ISqlServerDatabase sqlServerDatabase, ICriteriaValueRepository criteriaValueRepository) {
         _sqlServerDatabase = sqlServerDatabase;
         _criteriaValueRepository = criteriaValueRepository;
      }

      public List<LabelTranslationDbModel> GetAll(int idLabelTranslation) {
         var labelTranslations = new List<LabelTranslationDbModel>();
         var sqlCommand = new SqlCommand();

         string sqlQuery = @"
					SELECT * FROM LabelTranslation WHERE RefLabel = @IdLabelTranslation
			";

         sqlCommand.CommandText = sqlQuery;
         sqlCommand.Parameters.AddWithValue("@IdLabelTranslation", idLabelTranslation);

         IEnumerable<CustomDataRow> dataRowCollection = _sqlServerDatabase.GetCustomDataRowCollection(sqlCommand);

         foreach (var dataRow in dataRowCollection) {
            var labelTranslation = new LabelTranslationDbModel();

            labelTranslation.Id = ConversionHelper.ToInt(dataRow["IdLabelTranslation"]);
            labelTranslation.IsActive = ConversionHelper.ToBoolean(dataRow["IsActive"]);
            labelTranslation.RefLabel = ConversionHelper.ToInt(dataRow["RefLabel"]);
            labelTranslation.RefCriteriaValueLanguage = ConversionHelper.ToInt(dataRow["RefCriteriaValueLanguage"]);
            labelTranslation.Value = ConversionHelper.ToString(dataRow["Value"]);
            labelTranslation.DateTimeCreatedOn = ConversionHelper.ToDateTime(dataRow["DateTimeCreatedOn"]);

            labelTranslations.Add(labelTranslation);
         }

         return labelTranslations;
      }

      public LabelTranslationDbModel Get(int idLabelTranslation, string codeLanguage) {
         int idCriteriaValueLanguage = _criteriaValueRepository.GetId("LANGUAGE", codeLanguage);
         List<LabelTranslationDbModel> labelTranslations = GetAll(idLabelTranslation);

         return labelTranslations.FirstOrDefault(x => x.RefCriteriaValueLanguage == idCriteriaValueLanguage);
      }
   }
}