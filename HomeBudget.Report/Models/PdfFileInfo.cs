using System.Collections.Generic;

namespace HomeBudget.Report.Models {

   public class PdfFileInfo {
      public string Creator { get; set; }

      public string Producer { get; set; }

      public string CreationDate { get; set; }

      public string ModificationDate { get; set; }

      public List<string> EmbeddedFonts { get; set; }
   }
}