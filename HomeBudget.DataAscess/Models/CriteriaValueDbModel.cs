namespace HomeBudget.DataAccess.Models {

   public class CriteriaValueDbModel : BaseDbModel {
      public string CodeCriteriaValue { get; set; }

      public int SequenceOrder { get; set; }

      public int RefCriteria { get; set; }

      public int RefLabelCriteriaValueName { get; set; }
   }
}