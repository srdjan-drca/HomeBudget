using System;
using System.Collections.Generic;
using System.Drawing;
using HomeBudget.Report.Excel.Models;
using HomeBudget.Report.Extensions;
using HomeBudget.Report.Models;
using iTextSharp.text.pdf;
using OfficeOpenXml;

namespace HomeBudget.Report.Excel.Worksheets {

   public class AcroFieldsWorksheet : BaseWorksheet {
      private readonly string sheetName;

      private readonly List<string> columnNames;

      private readonly Dictionary<Tuple<string, int>, AcroFieldProperties> acroFields;

      public AcroFieldsWorksheet(BaseReportModel reportModel) {
         var acroFieldReportModel = (AcroFieldReportModel)reportModel;

         sheetName = acroFieldReportModel.SheetName;
         columnNames = CreateColumnNames();
         acroFields = acroFieldReportModel.AcroFields;
      }

      public override string Name {
         get { return sheetName; }
         set { }
      }

      public override void AddContent(ExcelWorksheet worksheet) {
         int rowId = 1;

         SetWorksheetStyle(worksheet, rowId);
         worksheet.Cells[rowId++, 1].SetValue(sheetName);

         worksheet.InsertLabels(columnNames, Color.LightBlue, rowId);

         foreach (var acroField in acroFields) {
            rowId++;
            int columnId = 1;

            worksheet.Cells[rowId, columnId++].SetValue(acroField.Value.Name);
            worksheet.Cells[rowId, columnId++].SetValue(acroField.Value.GetFieldTypeString());
            worksheet.Cells[rowId, columnId++].SetValue(acroField.Value.GetSelectOptionsString());
            worksheet.Cells[rowId, columnId++].SetValue(acroField.Value.Text.FontName);
            worksheet.Cells[rowId, columnId++].SetValue(acroField.Value.Text.FontSize);
            worksheet.Cells[rowId, columnId++].SetValue(acroField.Value.GetAlignmentString());
            worksheet.Cells[rowId, columnId++].SetValue(acroField.Value.PageNumber);
            worksheet.Cells[rowId, columnId++].SetValue(acroField.Value.LeftPos);
            worksheet.Cells[rowId, columnId++].SetValue(acroField.Value.BottomPos);
            worksheet.Cells[rowId, columnId++].SetValue(acroField.Value.TopPos);
            worksheet.Cells[rowId, columnId].SetValue(acroField.Value.RightPos);

            if (acroField.Value.Type == AcroFields.FIELD_TYPE_TEXT) {
               if (acroField.Value.Text.FontName == string.Empty || acroField.Value.Text.FontSize == 0) {
                  ExcelRange excelRange = worksheet.Cells[rowId, 1, rowId, columnId];
                  string comment = CreateComment(acroField);

                  excelRange.SetColor(Color.Tomato);
                  worksheet.Cells[rowId, columnId + 1].SetValue(comment);
               }
            }
         }

         worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
      }

      private List<string> CreateColumnNames() {
         var columnNameList = new List<string>
         {
            "Acrofield name",
            "Acrofield type",
            "Select options",
            "Font type",
            "Font size",
            "Text alignment",
            "Page number",
            "Left position",
            "Bottom position",
            "Top position",
            "Right position",
            "Comment"
         };

         return columnNameList;
      }

      private void SetWorksheetStyle(ExcelWorksheet worksheet, int rowId) {
         ExcelRange header = worksheet.Cells[rowId, 1, rowId, columnNames.Count];
         header.Merge = true;
         header.SetColor(Color.Yellow);
         header.SetBold();
      }

      private string CreateComment(KeyValuePair<Tuple<string, int>, AcroFieldProperties> acroField) {
         string comment = "Missing: ";
         var reasons = new List<string>();

         if (acroField.Value.Text.FontName == string.Empty) {
            reasons.Add("font name");
         }

         if (acroField.Value.Text.FontSize == 0) {
            reasons.Add("font size");
         }

         return comment + string.Join(", ", reasons);
      }
   }
}