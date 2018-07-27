using System.Collections.Generic;
using System.Linq;
using HomeBudget.Report.Models;
using HomeBudget.Tools.Extensions;
using HomeBudget.Tools.Helpers;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace HomeBudget.Report.Extensions {

   public static class ITextExtensions {

      private static readonly Dictionary<int, string> fieldTypeDict = new Dictionary<int, string>
      {
         { AcroFields.FIELD_TYPE_NONE, "None" },
         { AcroFields.FIELD_TYPE_PUSHBUTTON, "Push button" },
         { AcroFields.FIELD_TYPE_CHECKBOX, "Checkbox" },
         { AcroFields.FIELD_TYPE_RADIOBUTTON, "Radio button" },
         { AcroFields.FIELD_TYPE_TEXT, "Text" },
         { AcroFields.FIELD_TYPE_LIST, "List" },
         { AcroFields.FIELD_TYPE_COMBO, "Combo box" },
         { AcroFields.FIELD_TYPE_SIGNATURE, "Signature" }
      };

      private static readonly Dictionary<int, string> alignmentDict = new Dictionary<int, string>
      {
         { Element.ALIGN_LEFT, "Left" },
         { Element.ALIGN_CENTER, "Center" },
         { Element.ALIGN_RIGHT, "Right" }
      };

      public static bool IsCheckBox(this AcroFieldProperties acroFieldProps) {
         return acroFieldProps.Type == AcroFields.FIELD_TYPE_CHECKBOX;
      }

      public static bool IsRadioButton(this AcroFieldProperties acroFieldProps) {
         return acroFieldProps.Type == AcroFields.FIELD_TYPE_RADIOBUTTON;
      }

      public static string GetFieldTypeString(this AcroFieldProperties acroFieldProps) {
         string fieldTypeName = fieldTypeDict.GetValueOrDefault(acroFieldProps.Type);

         return fieldTypeName;
      }

      public static string GetAlignmentString(this AcroFieldProperties acroFieldProps) {
         string alignmentName = alignmentDict.GetValueOrDefault(acroFieldProps.Text.Alignment);

         return alignmentName;
      }

      public static string GetSelectOptionsString(this AcroFieldProperties acroFieldProps) {
         string selectOptions = acroFieldProps.SelectOptions.Any()
            ? "[ " + string.Join(" ", acroFieldProps.SelectOptions) + " ]"
            : string.Empty;

         return selectOptions;
      }

      public static PdfContentByte WriteContent(this PdfContentByte canvas, PdfPTable table, float x, float y, int rowStart = 0, int rowEnd = -1) {
         table.WriteSelectedRows(rowStart, rowEnd, x, y, canvas);

         return canvas;
      }

      public static PdfPCell SetHeight(this PdfPCell cell, float height) {
         cell.FixedHeight = height;

         return cell;
      }

      public static PdfPCell SetColspan(this PdfPCell cell, int colspan) {
         cell.Colspan = colspan;

         return cell;
      }

      public static List<string> GetSelectOptions(this AcroFields pdfForm, string acroFieldName) {
         List<string> selectOptions = pdfForm.GetAppearanceStates(acroFieldName).ToList();

         if (selectOptions.Any()) {
            selectOptions = selectOptions.MoveToTop(item => item == "Off");
         }

         return selectOptions;
      }

      public static TextProperties GetTextProperties(this AcroFields pdfForm, string acroFieldName) {
         AcroFields.Item acroField = pdfForm.GetFieldItem(acroFieldName);
         PdfDictionary merged = acroField.GetMerged(0);
         TextField textField = new TextField(null, null, null);

         pdfForm.DecodeGenericDictionary(merged, textField);

         return new TextProperties {
            FontName = textField.Font.With(x => x.FullFontName[0][3]),
            FontSize = textField.FontSize,
            Alignment = textField.Alignment
         };
      }

      public static AcroFields LockFields(this AcroFields pdfForm, string[] formFields) {
         foreach (var formField in formFields) {
            pdfForm.SetFieldProperty(formField, "setfflags", PdfFormField.FF_READ_ONLY, null);
         }

         return pdfForm;
      }
   }
}