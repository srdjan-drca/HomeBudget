using System.Collections.Generic;
using HomeBudget.DataAccess.Models;

namespace HomeBudget.DataAccess.Repositories.Interfaces {

   public interface ILabelTranslationRepository {

      List<LabelTranslationDbModel> GetAll(int idLabelTranslation);

      LabelTranslationDbModel Get(int idLabelTranslation, string codeLanguage);
   }
}