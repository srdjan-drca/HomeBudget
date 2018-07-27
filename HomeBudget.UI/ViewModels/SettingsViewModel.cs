using System.Collections.Generic;
using HomeBudget.DataAccess.Models;
using HomeBudget.DataAccess.Repositories.Interfaces;

namespace HomeBudget.ViewModels {

   public class SettingsViewModel {

      private readonly ICriteriaValueRepository _criteriaValueRepository;

      private readonly ILabelTranslationRepository _labelTranslationRepository;

      #region Binding properties

      public Dictionary<int, string> LanguageDropDown { get; set; }

      #endregion Binding properties

      public SettingsViewModel(ICriteriaValueRepository criteriaValueRepository, ILabelTranslationRepository labelTranslationRepository) {
         _criteriaValueRepository = criteriaValueRepository;
         _labelTranslationRepository = labelTranslationRepository;

         LanguageDropDown = CreateLanguageDropDown();
      }

      private Dictionary<int, string> CreateLanguageDropDown() {
         var languages = new Dictionary<int, string>();
         int monthNumber = 1;

         List<CriteriaValueDbModel> criteriaValues = _criteriaValueRepository.GetAllValues("LANGUAGE");

         foreach (var criteriaValue in criteriaValues) {
            string monthTranslation = _labelTranslationRepository.Get(criteriaValue.RefLabelCriteriaValueName, "SERBIAN").Value;

            languages.Add(monthNumber++, monthTranslation);
         }

         return languages;
      }
   }
}