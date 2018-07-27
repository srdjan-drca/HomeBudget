using System;
using System.Collections.Generic;
using HomeBudget.Report.Models;

namespace HomeBudget.Report.Excel.Models {

   public class ComparePdfReportModel : BaseReportModel {
      public Dictionary<Tuple<string, int>, AcroFieldProperties> AcroFieldsFirstPdf { get; set; }

      public Dictionary<Tuple<string, int>, AcroFieldProperties> AcroFieldsSecondPdf { get; set; }

      public List<Tuple<string, string>> FileNameWithDate { get; set; }

      public ComparePdfReportModel(Dictionary<Tuple<string, int>, AcroFieldProperties> acroFieldsFirstPdf,
         Dictionary<Tuple<string, int>, AcroFieldProperties> acroFieldsSecondPdf, List<Tuple<string, string>> fileNameWithDate) {
         AcroFieldsFirstPdf = acroFieldsFirstPdf;
         AcroFieldsSecondPdf = acroFieldsSecondPdf;
         FileNameWithDate = fileNameWithDate;
      }
   }
}