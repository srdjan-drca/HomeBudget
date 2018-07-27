using System;
using System.Collections.Generic;
using HomeBudget.Report.Models;

namespace HomeBudget.Report.Excel.Models {

   public class AcroFieldReportModel : BaseReportModel {
      public string SheetName { get; set; }

      public Dictionary<Tuple<string, int>, AcroFieldProperties> AcroFields { get; set; }

      public AcroFieldReportModel(string sheetName, Dictionary<Tuple<string, int>, AcroFieldProperties> acroFields) {
         SheetName = sheetName;
         AcroFields = acroFields;
      }
   }
}