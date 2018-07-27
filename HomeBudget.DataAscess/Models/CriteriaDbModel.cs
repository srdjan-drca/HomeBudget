namespace HomeBudget.DataAccess.Models {

   public class CriteriaDbModel : BaseDbModel {
      public string CodeCriteria { get; set; }

      public int SequenceOrder { get; set; }

      public int RefLabelCriteriaName { get; set; }
   }
}