using OfficeOpenXml;

namespace HomeBudget.Report.Excel.Worksheets {

   public abstract class BaseWorksheet {
      public abstract string Name { get; set; }

      public abstract void AddContent(ExcelWorksheet worksheet);
   }
}