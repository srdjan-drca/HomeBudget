using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HomeBudget.Command;
using HomeBudget.DataAccess.Models;
using HomeBudget.DataAccess.Repositories.Interfaces;
using HomeBudget.Tools.Helpers;

namespace HomeBudget.ViewModels {

   public class NewCostsViewModel : BaseViewModel {

      private readonly ICriteriaValueRepository _criteriaValueRepository;

      private readonly ILabelTranslationRepository _labelTranslationRepository;

      private readonly ICostRepository _costRepository;

      #region Binding properties

      public Dictionary<int, string> CostTypeDropDown {
         get {
            var costTypeDropDown = new Dictionary<int, string>();
            List<CriteriaValueDbModel> criteriaValueCostTypes = _criteriaValueRepository.GetAllValues("COST_TYPE").ToList();
            string languageCode = "SERBIAN";

            foreach (var criteriaValueCostType in criteriaValueCostTypes) {
               int costTypeLabelId = criteriaValueCostType.RefLabelCriteriaValueName;
               var costTypeTranslation = _labelTranslationRepository.Get(costTypeLabelId, languageCode).Value;

               costTypeDropDown.Add(criteriaValueCostType.Id, costTypeTranslation);
            }

            return costTypeDropDown;
         }
      }

      public string StatusMessage { get; set; }

      public ICommand SaveCostCommand { get; }

      #endregion Binding properties

      public NewCostsViewModel(ICriteriaValueRepository criteriaValueRepository,
         ILabelTranslationRepository labelTranslationRepository, ICostRepository costRepository) {

         _criteriaValueRepository = criteriaValueRepository;
         _labelTranslationRepository = labelTranslationRepository;
         _costRepository = costRepository;

         SaveCostCommand = new RelayCommand(param => SaveCost(param));
      }

      private void SaveCost(object parameter) {
         var values = (object[])parameter;
         var cost = new CostDbModel {
            IsActive = true,
            RefCriteriaValueCostType = ConversionHelper.ToInt(values[0]),
            Amount = ConversionHelper.ToFloat(values[1]),
            DateTimeCreatedOn = ConversionHelper.ToDateTime(values[2])
         };

         _costRepository.Save(cost);

         StatusMessage = "Cost saved succesfully!";
      }
   }
}