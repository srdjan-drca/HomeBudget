﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HomeBudget.Report.Extensions;
using HomeBudget.Report.Models;
using HomeBudget.Tools.Extensions;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace HomeBudget.Report.Helpers {

   public static class PdfHelper {
      private static Font frutigerLight5;

      public static void InitializeIText() {
         PdfReader.unethicalreading = true;

         RegisterFonts();
      }

      public static int GetNumberOfPages(string fullFileName) {
         PdfReader pdfReader = new PdfReader(fullFileName);
         int pageNumber = pdfReader.NumberOfPages;

         pdfReader.Close();

         return pageNumber;
      }

      public static PdfFileInfo GetFileInfo(string fullFileName) {
         PdfReader pdfReader = new PdfReader(fullFileName);
         List<object[]> fonts = BaseFont.GetDocumentFonts(pdfReader);
         string creationDate = pdfReader.Info.GetValueOrDefault("CreationDate");
         string modificationDate = pdfReader.Info.GetValueOrDefault("ModDate");

         var fileInfo = new PdfFileInfo {
            Creator = pdfReader.Info.GetValueOrDefault("Creator"),
            Producer = pdfReader.Info.GetValueOrDefault("Producer"),
            CreationDate = ParseDateTime(creationDate),
            ModificationDate = ParseDateTime(modificationDate),
            EmbeddedFonts = CreateEmbeddedFontsList(fonts)
         };

         pdfReader.Close();

         return fileInfo;
      }

      public static Dictionary<Tuple<string, int>, AcroFieldProperties> GetAcroFields(string fileName) {
         Dictionary<Tuple<string, int>, AcroFieldProperties> acroFields = new Dictionary<Tuple<string, int>, AcroFieldProperties>();
         int numberOfPages = GetNumberOfPages(fileName);

         for (int pageNumber = 1; pageNumber <= numberOfPages; pageNumber++) {
            Dictionary<Tuple<string, int>, AcroFieldProperties> acroFieldsByPage = GetAcroFields(fileName, pageNumber);
            acroFields.AddRange(acroFieldsByPage);
         }

         return acroFields;
      }

      public static Dictionary<Tuple<string, int>, AcroFieldProperties> GetAcroFields(string fileName, int pageNumber) {
         Dictionary<Tuple<string, int>, AcroFieldProperties> acroFields = new Dictionary<Tuple<string, int>, AcroFieldProperties>();
         PdfReader pdfReader = new PdfReader(fileName);

         if (pageNumber > 0 && pageNumber <= pdfReader.NumberOfPages) {
            pdfReader.SelectPages(pageNumber.ToString());
            AcroFields pdfForm = pdfReader.AcroFields;

            foreach (string acroFieldName in pdfForm.Fields.Keys) {
               AcroFieldProperties acroFieldProperties = GetAcroFieldProperties(pdfForm, acroFieldName, pageNumber);

               acroFields.Add(new Tuple<string, int>(acroFieldName, pageNumber), acroFieldProperties);
            }
         }

         pdfReader.Close();

         return acroFields;
      }

      public static void LabelAcroFields(string fileName, string outputFileName, List<AcroFieldProperties> acroFieldsProps) {
         PdfReader reader = new PdfReader(fileName);
         FileStream output = new FileStream(outputFileName, FileMode.Create, FileAccess.Write);
         PdfStamper stamp = new PdfStamper(reader, output);

         foreach (var acroFieldProp in acroFieldsProps) {
            if (acroFieldProp.IsCheckBox() || acroFieldProp.IsRadioButton()) {
               var canvas = stamp.GetOverContent(acroFieldProp.PageNumber);
               float height = acroFieldProp.TopPos - acroFieldProp.BottomPos;

               canvas.WriteContent(CreateAcroFieldNameTable(acroFieldProp), acroFieldProp.LeftPos, acroFieldProp.BottomPos + 1.5f * height);
            }
            else {
               stamp.AcroFields.SetFieldProperty(acroFieldProp.Name, "textfont", frutigerLight5.BaseFont, null);
               stamp.AcroFields.SetFieldProperty(acroFieldProp.Name, "textcolor", BaseColor.RED, null);
               stamp.AcroFields.SetFieldProperty(acroFieldProp.Name, "textsize", 5f, null);
               stamp.AcroFields.SetField(acroFieldProp.Name, acroFieldProp.Name);
            }
         }

         stamp.Close();
      }

      public static PdfPTable CreateTable(params float[] columnsWidth) {
         PdfPTable table = new PdfPTable(columnsWidth.Length);

         table.DefaultCell.Border = Rectangle.NO_BORDER;
         table.SetWidths(columnsWidth);
         table.TotalWidth = columnsWidth.Sum();
         table.LockedWidth = true;
         table.DefaultCell.Padding = 0;

         return table;
      }

      public static PdfPCell CreateCell(string content) {
         PdfPCell cell = new PdfPCell(new Paragraph(content, frutigerLight5)) {
            PaddingTop = 0,
            PaddingBottom = 0,
            PaddingLeft = 0,
            PaddingRight = 0,
            HorizontalAlignment = Element.ALIGN_LEFT,
            VerticalAlignment = Element.ALIGN_MIDDLE,
            BackgroundColor = BaseColor.WHITE,
            BorderColor = BaseColor.WHITE,
            Border = Rectangle.NO_BORDER
         };

         return cell;
      }

      #region Private methods

      private static void RegisterFonts() {
         string rootPath = Path.Combine(TryGetSolutionDirectoryInfo().FullName, "HomeBudget.Report\\Resources\\");
         string frutigerLt45Path = Path.Combine(rootPath, "LTe50327.ttf");
         string frutigerLt65Path = Path.Combine(rootPath, "lte50329.ttf");

         // Load from win fonts directory, but those ttf will be included in project and will be read from resources
         FontFactory.Register(frutigerLt45Path, "frutiger lt 45 light");
         FontFactory.Register(frutigerLt65Path, "frutiger lt 65 bold");

         frutigerLight5 = FontFactory.GetFont("frutiger lt 45 light", BaseFont.WINANSI, BaseFont.EMBEDDED, 5, Font.NORMAL, BaseColor.RED);
      }

      private static PdfPTable CreateAcroFieldNameTable(AcroFieldProperties acroFieldProperties) {
         string nameWithOptions = acroFieldProperties.Name + " " + acroFieldProperties.GetSelectOptionsString();
         PdfPTable table = CreateTable(200f);
         PdfPCell cell = CreateCell(nameWithOptions);

         table.AddCell(cell);

         return table;
      }

      private static List<string> CreateEmbeddedFontsList(List<object[]> fonts) {
         return fonts.Select(font => font[0].ToString()).ToList();
      }

      private static AcroFieldProperties GetAcroFieldProperties(AcroFields pdfForm, string acroFieldName, int pageNumber) {
         IList<AcroFields.FieldPosition> position = pdfForm.GetFieldPositions(acroFieldName);

         return new AcroFieldProperties {
            Name = acroFieldName,
            Type = pdfForm.GetFieldType(acroFieldName),
            SelectOptions = pdfForm.GetSelectOptions(acroFieldName),
            Text = pdfForm.GetTextProperties(acroFieldName),
            PageNumber = pageNumber,
            LeftPos = position[0].position.Left,
            BottomPos = position[0].position.Bottom,
            RightPos = position[0].position.Right,
            TopPos = position[0].position.Top
         };
      }

      private static string ParseDateTime(string creationDate) {
         string year = creationDate.Substring(2, 4);
         string month = creationDate.Substring(6, 2);
         string day = creationDate.Substring(8, 2);
         string hour = creationDate.Substring(10, 2);
         string minute = creationDate.Substring(12, 2);
         string second = creationDate.Substring(14, 2);
         StringBuilder dateTime = new StringBuilder();

         dateTime.AppendFormat("{0}/", day);
         dateTime.AppendFormat("{0}/", month);
         dateTime.AppendFormat("{0} ", year);
         dateTime.AppendFormat("{0}:", hour);
         dateTime.AppendFormat("{0}:", minute);
         dateTime.AppendFormat("{0}", second);

         return dateTime.ToString();
      }

      public static DirectoryInfo TryGetSolutionDirectoryInfo(string currentPath = null) {
         var directory = new DirectoryInfo(
             currentPath ?? Directory.GetCurrentDirectory());
         while (directory != null && !directory.GetFiles("*.sln").Any()) {
            directory = directory.Parent;
         }

         return directory;
      }

      #endregion Private methods
   }
}