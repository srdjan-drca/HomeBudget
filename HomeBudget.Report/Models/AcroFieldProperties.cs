using System.Collections.Generic;

namespace HomeBudget.Report.Models {

   public class AcroFieldProperties {
      public string Name { get; set; }

      public int Type { get; set; }

      public List<string> SelectOptions { get; set; }

      public TextProperties Text { get; set; }

      public int PageNumber { get; set; }

      public float LeftPos { get; set; }

      public float BottomPos { get; set; }

      public float RightPos { get; set; }

      public float TopPos { get; set; }
   }
}