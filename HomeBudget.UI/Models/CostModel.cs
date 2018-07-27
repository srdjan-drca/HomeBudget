using System;

namespace HomeBudget.Models {

   public class CostModel {
      public string CostType { get; set; }

      public float Amount { get; set; }

      public DateTime CreatedOn { get; set; }

      public CostModel(string costType, float amount, DateTime createdOn) {
         CostType = costType;
         Amount = amount;
         CreatedOn = createdOn;
      }
   }
}