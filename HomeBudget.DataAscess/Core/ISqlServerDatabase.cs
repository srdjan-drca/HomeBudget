using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HomeBudget.DataAccess.Core {

   public interface ISqlServerDatabase : IDatabase {

      DataTable GetDataTableReadUncomited(SqlCommand sqlCommand, SqlTransaction transaction, Action logAction = null);

      DataSet GetDataSetReadUncomited(SqlCommand sqlCommand, SqlTransaction transaction, Action logAction = null);

      DataRowCollection GetDataRowCollection(SqlCommand sqlCommand, Action logAction = null);

      IEnumerable<CustomDataRow> GetCustomDataRowCollection(SqlCommand sqlCommand, Action logAction = null);

      DataRow GetDataRow(SqlCommand sqlCommand, Action logAction = null);

      CustomDataRow GetCustomDataRow(SqlCommand sqlCommand, Action logAction = null);

      DataRow GetDataRowReadUncomited(SqlCommand sqlCommand, SqlTransaction transaction, Action logAction = null);

      CustomDataRow GetCustomDataRowReadUncomited(SqlCommand sqlCommand, SqlTransaction transaction, Action logAction = null);

      DataRowCollection GetDataRowCollectionReadUncomited(SqlCommand sqlCommand, SqlTransaction transaction,
         Action logAction = null);

      IEnumerable<CustomDataRow> GetCustomDataRowCollectionReadUncomited(SqlCommand sqlCommand, SqlTransaction transaction,
         Action logAction = null);

      object GetScalarValue(SqlCommand sqlCommand);

      void ExecuteNonQuery(SqlCommand sqlCommand);

      void ExecuteNonQueryInTransaction(SqlCommand sqlCommand);

      object GetScalarValueInTransaction(SqlCommand sqlCommand);

      DataRow GetDataRowInTransaction(SqlCommand sqlCommand);

      DataRowCollection GetDataRowCollectionInTransaction(SqlCommand sqlCommand);

      DataTable GetDataTableInTransaction(SqlCommand sqlCommand);

      SqlTransaction StartTransaction();

      SqlTransaction StartTransaction(IsolationLevel isolationLevel);

      void EndTransaction(SqlTransaction transaction);

      void RollbackTransaction(SqlTransaction transaction);
   }
}