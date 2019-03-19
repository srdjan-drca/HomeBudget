using System.Collections.Generic;
using System.Linq;
using HomeBudget.DataAccess.Models;
using HomeBudget.DataAccess.Repositories.Interfaces;
using HomeBudget.Models;

namespace HomeBudget.ViewModels {

   public class AllCostsViewModel : BaseViewModel {

      private readonly ICostRepository _costRepository;

      private readonly ICriteriaValueRepository _criteriaValueRepository;

      private readonly ILabelTranslationRepository _labelTranslationRepository;

      #region Binding properties

      public int Year { get; set; }

      private Dictionary<int, string> _monthsDropDown;

      public Dictionary<int, string> MonthsDropDown {
         get {
            return _monthsDropDown;
         }
         set {
            var test = value;
         }
      }

      public List<CostModel> Costs {
         get {
            var dbCosts = _costRepository.GetAll();
            var costs = dbCosts.Select(x =>
               new CostModel(ToCostTypeString(x.RefCriteriaValueCostType), x.Amount, x.DateTimeCreatedOn.Value))
               .ToList();

            return costs;
         }
      }

      #endregion Binding properties

      public AllCostsViewModel(ICostRepository costRepository,
         ICriteriaValueRepository criteriaValueRepository, ILabelTranslationRepository labelTranslationRepository) {

         _costRepository = costRepository;
         _criteriaValueRepository = criteriaValueRepository;
         _labelTranslationRepository = labelTranslationRepository;

         Year = 2018;
         _monthsDropDown = CreateMonthsDropDown();
      }

      private string ToCostTypeString(int criteriaValue) {
         if (criteriaValue == 3) {
            return "Bill servicing";
         }

         if (criteriaValue == 4) {
            return "Masterata servicing";
         }

         if (criteriaValue == 5) {
            return "Home insurance";
         }

         if (criteriaValue == 6) {
            return "Housing loan";
         }

         if (criteriaValue == 7) {
            return "Masterata";
         }

         return string.Empty;
      }

      private Dictionary<int, string> CreateMonthsDropDown() {
         var months = new Dictionary<int, string>();
         int monthNumber = 1;

         List<CriteriaValueDbModel> criteriaValues = _criteriaValueRepository.GetAllValues("MONTH");

         foreach (var criteriaValue in criteriaValues) {
            string monthTranslation = _labelTranslationRepository.Get(criteriaValue.RefLabelCriteriaValueName, "SERBIAN").Value;

            months.Add(monthNumber++, monthTranslation);
         }

         return months;
      }
   }
}