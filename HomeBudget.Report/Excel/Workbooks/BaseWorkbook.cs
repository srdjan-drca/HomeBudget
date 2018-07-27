using System.Collections.Generic;
using HomeBudget.Report.Excel.Models;
using HomeBudget.Report.Excel.Worksheets;

namespace HomeBudget.Report.Excel.Workbooks {

   public abstract class BaseWorkbook {
      private readonly List<BaseWorksheet> worksheets = new List<BaseWorksheet>();

      public List<BaseWorksheet> Worksheets => worksheets;

      protected BaseWorkbook(IEnumerable<BaseReportModel> baseReportModels) {
         CreateWorksheets(baseReportModels);
      }

      protected abstract void CreateWorksheets(IEnumerable<BaseReportModel> baseReportModels);
   }
}