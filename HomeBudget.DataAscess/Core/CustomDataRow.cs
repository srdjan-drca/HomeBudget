using System;
using System.Collections.Generic;

namespace HomeBudget.DataAccess.Core {

   public class CustomDataRow {
      private readonly Dictionary<string, int> columnNameToIndexMapping;

      private readonly object[] values;

      public CustomDataRow(Dictionary<string, int> columnNameToIndexMapping, object[] values) {
         this.columnNameToIndexMapping = columnNameToIndexMapping;
         this.values = values;
      }

      public object this[string key] {
         get {
            int columnIndex;
            if (columnNameToIndexMapping.TryGetValue(key, out columnIndex)) {
               return values[columnIndex];
            }

            throw new Exception(string.Format("Column '{0}' does not belong to table.", key));
         }
      }
   }
}