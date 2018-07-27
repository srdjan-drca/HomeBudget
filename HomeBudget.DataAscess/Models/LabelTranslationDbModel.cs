namespace HomeBudget.DataAccess.Models {

   public class LabelTranslationDbModel : BaseDbModel {
      public int RefLabel { get; set; }

      public int RefCriteriaValueLanguage { get; set; }

      public string Value { get; set; }
   }
}