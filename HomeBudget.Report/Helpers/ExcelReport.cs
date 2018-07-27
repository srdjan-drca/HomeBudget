using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HomeBudget.Report.Excel.Models;
using HomeBudget.Report.Excel.Workbooks;
using HomeBudget.Report.Extensions;
using HomeBudget.Report.Models;
using OfficeOpenXml;

namespace HomeBudget.Report.Helpers {

   public static class ExcelHelper {

      public static string GenerateAcroFieldsReport(string reportFileName, List<string> pdfFilePaths) {
         string reportFullName = CreateReportName(reportFileName);
         FileStream reportFile = File.Create(reportFullName);
         List<AcroFieldReportModel> acroFieldReportModels = CreateAcroFieldReportModels(pdfFilePaths);
         var acroFieldReportWorkbook = new AcroFieldsWorkbook(acroFieldReportModels);
         int workSheetCounter = 0;

         using (var package = new ExcelPackage(reportFile)) {
            foreach (var worksheet in acroFieldReportWorkbook.Worksheets) {
               workSheetCounter++;

               var worksheetName = workSheetCounter.ToString();
               var excelWorksheet = package.Workbook.AddWorksheet(worksheetName);

               worksheet.AddContent(excelWorksheet);
            }

            package.Save();
         }

         reportFile.Close();

         return reportFullName;
      }

      public static string GenerateComparePdfReport(string reportFileName, string firstPdf, string secondPdf) {
         string reportFullName = CreateReportName(reportFileName);
         FileStream reportFile = File.Create(reportFullName);
         List<ComparePdfReportModel> comparePdfReportModel = CreateComparePdfReportModel(firstPdf, secondPdf);
         var comparePdfWorkbook = new ComparePdfWorkbook(comparePdfReportModel);

         using (var package = new ExcelPackage(reportFile)) {
            foreach (var worksheet in comparePdfWorkbook.Worksheets) {
               var excelWorksheet = package.Workbook.AddWorksheet(worksheet.Name);

               worksheet.AddContent(excelWorksheet);
            }

            package.Save();
         }

         reportFile.Close();

         return reportFullName;
      }

      #region Private methods

      private static List<AcroFieldReportModel> CreateAcroFieldReportModels(List<string> pdfFilePaths) {
         var excelReportModels = new List<AcroFieldReportModel>();

         foreach (var fullPdfFileName in pdfFilePaths) {
            var pdfFileName = fullPdfFileName.Split('\\').LastOrDefault().Split('.').FirstOrDefault();
            var acroFields = PdfHelper.GetAcroFields(fullPdfFileName);

            excelReportModels.Add(new AcroFieldReportModel(pdfFileName, acroFields));
         }

         return excelReportModels;
      }

      private static List<ComparePdfReportModel> CreateComparePdfReportModel(string firstPdfName, string secondPdfName) {
         Dictionary<Tuple<string, int>, AcroFieldProperties> firstPdf = PdfHelper.GetAcroFields(firstPdfName);
         Dictionary<Tuple<string, int>, AcroFieldProperties> secondPdf = PdfHelper.GetAcroFields(secondPdfName);
         List<Tuple<string, string>> fileNameWithDate = CreateFileNameWithDate(firstPdfName, secondPdfName);

         var excelReportModels = new List<ComparePdfReportModel>
         {
            new ComparePdfReportModel(firstPdf, secondPdf, fileNameWithDate)
         };

         return excelReportModels;
      }

      private static List<Tuple<string, string>> CreateFileNameWithDate(string firstPdfName, string secondPdfName) {
         string creationDate1 = PdfHelper.GetFileInfo(firstPdfName).CreationDate;
         string creationDate2 = PdfHelper.GetFileInfo(secondPdfName).CreationDate;

         var infos = new List<Tuple<string, string>>
         {
            Tuple.Create(firstPdfName, creationDate1),
            Tuple.Create(secondPdfName, creationDate2)
         };

         return infos;
      }

      private static string CreateReportName(string reportPrefix) {
         StringBuilder reportName = new StringBuilder();
         DateTime currentDateTime = DateTime.Now;

         reportName.Append(reportPrefix);
         reportName.Append("_");
         reportName.Append(currentDateTime.Hour);
         reportName.Append("_");
         reportName.Append(currentDateTime.Minute);
         reportName.Append("_");
         reportName.Append(currentDateTime.Second);
         reportName.Append(".xlsx");

         return reportName.ToString();
      }

      #endregion Private methods
   }
}