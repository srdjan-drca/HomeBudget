using System.Collections.Generic;
using System.Linq;
using HomeBudget.Report.Excel.Models;
using HomeBudget.Report.Excel.Worksheets;

namespace HomeBudget.Report.Excel.Workbooks {

   public class ComparePdfWorkbook : BaseWorkbook {

      public ComparePdfWorkbook(IEnumerable<BaseReportModel> baseReportModels) : base(baseReportModels) {
      }

      protected override void CreateWorksheets(IEnumerable<BaseReportModel> baseReportModels) {
         Worksheets.Add(new ComparePdfWorksheet(baseReportModels.FirstOrDefault()));
      }
   }
}