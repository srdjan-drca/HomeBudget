using System.Collections.Generic;
using HomeBudget.Report.Excel.Models;
using HomeBudget.Report.Excel.Worksheets;

namespace HomeBudget.Report.Excel.Workbooks {

   public class AcroFieldsWorkbook : BaseWorkbook {

      public AcroFieldsWorkbook(IEnumerable<BaseReportModel> baseReportModels) : base(baseReportModels) {
      }

      protected override void CreateWorksheets(IEnumerable<BaseReportModel> baseReportModels) {
         foreach (var baseReportModel in baseReportModels) {
            Worksheets.Add(new AcroFieldsWorksheet(baseReportModel));
         }
      }
   }
}