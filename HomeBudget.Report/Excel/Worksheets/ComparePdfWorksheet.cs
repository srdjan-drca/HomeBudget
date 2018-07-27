using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using HomeBudget.Report.Excel.Models;
using HomeBudget.Report.Extensions;
using HomeBudget.Report.Helpers;
using HomeBudget.Report.Models;
using HomeBudget.Tools.Extensions;
using HomeBudget.Tools.Helpers;
using OfficeOpenXml;

namespace HomeBudget.Report.Excel.Worksheets {

   public class ComparePdfWorksheet : BaseWorksheet {
      private readonly string sheetName;

      private readonly List<string> columnNames;

      private readonly ComparePdfReportModel reportModel;

      public override string Name {
         get { return sheetName; }
         set { }
      }

      public ComparePdfWorksheet(BaseReportModel reportModel) {
         this.reportModel = (ComparePdfReportModel)reportModel;

         List<string> documentDates = this.reportModel.FileNameWithDate.Select(x => x.Item2).ToList();

         sheetName = "ComparePdf";
         columnNames = CreateColumnNames(documentDates);
      }

      public override void AddContent(ExcelWorksheet worksheet) {
         var acroFieldsFirstPdf = reportModel.AcroFieldsFirstPdf;
         var acroFieldsSecondPdf = reportModel.AcroFieldsSecondPdf;
         List<Tuple<string, int>> referentAcroFields = acroFieldsFirstPdf.Select(x => x.Key).Union(acroFieldsSecondPdf.Select(x => x.Key)).ToList();
         int rowId = 1;

         rowId = CreateHeader(worksheet, rowId);

         foreach (Tuple<string, int> referentAcroField in referentAcroFields) {
            rowId++;
            int columnId = 1;

            AcroFieldProperties acroFieldPropsFirstPdf = acroFieldsFirstPdf.GetValueOrDefault(referentAcroField);
            AcroFieldProperties acroFieldPropsSecondPdf = acroFieldsSecondPdf.GetValueOrDefault(referentAcroField);

            if (acroFieldPropsFirstPdf != null && acroFieldPropsSecondPdf != null) {
               worksheet.Cells[rowId, columnId++].SetValue(acroFieldPropsFirstPdf.With(x => x.Name));
               worksheet.Cells[rowId, columnId++].SetValue(acroFieldPropsFirstPdf.With(x => x.PageNumber));
               worksheet.Cells[rowId, columnId++].SetValue(Constants.FieldOk);
               worksheet.Cells[rowId, columnId].SetValue(Constants.FieldOk);
            }
            else if (acroFieldPropsFirstPdf != null) {
               worksheet.Cells[rowId, columnId++].SetValue(acroFieldPropsFirstPdf.With(x => x.Name));
               worksheet.Cells[rowId, columnId++].SetValue(acroFieldPropsFirstPdf.With(x => x.PageNumber));
               worksheet.Cells[rowId, columnId].SetValue(Constants.FieldRemoved);

               ExcelRange excelRange = worksheet.Cells[rowId, 1, rowId, 4];
               excelRange.SetColor(Color.OrangeRed);
            }
            else if (CheckHelper.IsFilled(acroFieldPropsSecondPdf)) {
               worksheet.Cells[rowId, columnId++].SetValue(acroFieldPropsSecondPdf.With(x => x.Name));
               worksheet.Cells[rowId, columnId++].SetValue(acroFieldPropsSecondPdf.With(x => x.PageNumber));
               worksheet.Cells[rowId, columnId + 1].SetValue(Constants.FieldAdded);

               ExcelRange excelRange = worksheet.Cells[rowId, 1, rowId, 4];
               excelRange.SetColor(Color.LimeGreen);
            }
            else {
               throw new Exception("Unexpected acro field");
            }
         }

         worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
      }

      private int CreateHeader(ExcelWorksheet worksheet, int rowId) {
         var pdfInfo = reportModel.FileNameWithDate.First();
         var pdfInfo2 = reportModel.FileNameWithDate.Last();

         worksheet.Cells[rowId, 1].SetValue(pdfInfo.Item2);
         worksheet.Cells[rowId, 2].SetValue(pdfInfo.Item1);
         worksheet.Cells[rowId, 2, rowId, 10].Merge = true;
         rowId++;

         worksheet.Cells[rowId, 1].SetValue(pdfInfo2.Item2);
         worksheet.Cells[rowId, 2].SetValue(pdfInfo2.Item1);
         worksheet.Cells[rowId, 2, rowId, 10].Merge = true;
         rowId++;

         worksheet.InsertLabels(columnNames, Color.LightBlue, rowId);

         return rowId;
      }

      private List<string> CreateColumnNames(List<string> documentDates) {
         var columnNameList = new List<string>
         {
            "Acrofield name",
            "Page number"
         };
         columnNameList.AddRange(documentDates);

         return columnNameList;
      }
   }
}