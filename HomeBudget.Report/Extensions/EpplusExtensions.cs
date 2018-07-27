using System;
using System.Collections.Generic;
using System.Drawing;
using HomeBudget.Tools.Helpers;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace HomeBudget.Report.Extensions {

   public static class EpplusExtensions {

      public static ExcelWorksheet AddWorksheet(this ExcelWorkbook workbook, string worksheetName) {
         ExcelWorksheet worksheet = workbook.Worksheets.Add(worksheetName);

         return worksheet;
      }

      public static void InsertLabels(this ExcelWorksheet worksheet, List<string> headerLabels, Color backgroundColor, int rowId = 1, int columnId = 1) {
         int fromColumn = columnId;

         foreach (var columnLabel in headerLabels) {
            worksheet.Cells[rowId, columnId++].Value = columnLabel;
         }

         ExcelRange excelRange = worksheet.Cells[rowId, fromColumn, rowId, columnId - 1];

         excelRange.Style.Font.Bold = true;
         excelRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
         excelRange.Style.Fill.BackgroundColor.SetColor(backgroundColor);
      }

      public static void SetValue(this ExcelRange excelRange, float value, bool showValueIfZero = false, string valueIfZero = null) {
         if (showValueIfZero || value > 0) {
            excelRange.Value = value;
         }
         else {
            if (valueIfZero != null) {
               excelRange.Value = valueIfZero;
            }
         }
      }

      public static void SetValue(this ExcelRange excelRange, float value, string numberFormat, string valueIfZero = null) {
         if (value > 0) {
            excelRange.Value = value;
            excelRange.Style.Numberformat.Format = numberFormat;
         }
         else {
            if (valueIfZero != null) {
               excelRange.Value = valueIfZero;
            }
         }
      }

      public static void SetValue(this ExcelRange excelRange, string value) {
         if (CheckHelper.IsFilled(value)) {
            excelRange.Value = value;
         }
      }

      public static void SetValue(this ExcelRange excelRange, string value, string numberFormat) {
         if (CheckHelper.IsFilled(value)) {
            excelRange.Value = value;
            excelRange.Style.Numberformat.Format = numberFormat;
         }
      }

      public static void SetValue(this ExcelRange excelRange, DateTime? value) {
         if (CheckHelper.IsFilled(value)) {
            excelRange.Value = value;
         }
      }

      public static void SetValue(this ExcelRange excelRange, DateTime? value, string numberFormat) {
         if (CheckHelper.IsFilled(value)) {
            excelRange.Value = value;
            excelRange.Style.Numberformat.Format = numberFormat;
         }
      }

      public static void SetColor(this ExcelRange excelRange, Color color) {
         excelRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
         excelRange.Style.Fill.BackgroundColor.SetColor(color);
      }

      public static void SetBold(this ExcelRange excelRange) {
         excelRange.Style.Font.Bold = true;
      }
   }
}