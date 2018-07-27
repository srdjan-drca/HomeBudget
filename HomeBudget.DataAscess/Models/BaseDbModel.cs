using System;

namespace HomeBudget.DataAccess.Models {

   public class BaseDbModel {
      public int Id { get; set; }

      public bool IsActive { get; set; }

      public DateTime? DateTimeCreatedOn { get; set; }
   }
}