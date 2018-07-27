using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using HomeBudget.DataAccess.Configuration;

namespace HomeBudget.DataAccess.Core {

   public class SqlServerDatabase : ISqlServerDatabase {

      private readonly string _connectionString;

      private readonly bool _enableConnectionStatistics;

      public SqlServerDatabase(IDatabaseConfiguration configuration) {
         _connectionString = configuration.ConnectionString;
         _enableConnectionStatistics = configuration.EnableConnectionStatistics;
      }

      public DataTable GetDataTable(SqlCommand sqlCommand, Action logAction = null) {
         var dataTable = new DataTable();

         using (SqlConnection sqlConnection = CreateSqlConnection()) {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            dataTable.Load(sqlCommand.ExecuteReader());
         }

         return dataTable;
      }

      public DataTable GetDataTableReadUncomited(SqlCommand sqlCommand, SqlTransaction transaction, Action logAction = null) {
         var dataTable = new DataTable();

         using (SqlConnection sqlConnection = CreateSqlConnection()) {
            sqlConnection.Open();
            sqlCommand.Connection = transaction.Connection;
            sqlCommand.Transaction = transaction;
            dataTable.Load(sqlCommand.ExecuteReader());
         }

         return dataTable;
      }

      public DataSet GetDataSetReadUncomited(SqlCommand sqlCommand, SqlTransaction transaction, Action logAction = null) {
         var dataSet = new DataSet();

         using (SqlConnection sqlConnection = CreateSqlConnection()) {
            sqlConnection.Open();
            sqlCommand.Connection = transaction.Connection;
            sqlCommand.Transaction = transaction;

            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand)) {
               sqlDataAdapter.Fill(dataSet);
            }
         }

         return dataSet;
      }

      public DataRowCollection GetDataRowCollection(SqlCommand sqlCommand, Action logAction = null) {
         DataTable dataTable;
         DataRowCollection dataRows = null;

         dataTable = GetDataTable(sqlCommand, logAction);
         if (dataTable.Rows.Count > 0) {
            dataRows = dataTable.Rows;
         }

         return dataRows;
      }

      public IEnumerable<CustomDataRow> GetCustomDataRowCollection(SqlCommand sqlCommand, Action logAction = null) {
         return GetDataFromSqlDataReader(sqlCommand, logAction: logAction);
      }

      public DataRow GetDataRow(SqlCommand sqlCommand, Action logAction = null) {
         DataTable dataTable;
         DataRow dataRow = null;

         dataTable = GetDataTable(sqlCommand, logAction);
         if (dataTable.Rows.Count > 0) {
            dataRow = dataTable.Rows[0];
         }

         return dataRow;
      }

      public CustomDataRow GetCustomDataRow(SqlCommand sqlCommand, Action logAction = null) {
         return GetDataFromSqlDataReader(sqlCommand, logAction: logAction).FirstOrDefault();
      }

      public DataRow GetDataRowReadUncomited(SqlCommand sqlCommand, SqlTransaction transaction, Action logAction = null) {
         DataTable dataTable;
         DataRow dataRow = null;

         dataTable = GetDataTableReadUncomited(sqlCommand, transaction, logAction);
         if (dataTable.Rows.Count > 0) {
            dataRow = dataTable.Rows[0];
         }

         return dataRow;
      }

      public CustomDataRow GetCustomDataRowReadUncomited(SqlCommand sqlCommand, SqlTransaction transaction, Action logAction = null) {
         return GetDataFromSqlDataReader(sqlCommand, transaction, logAction).FirstOrDefault();
      }

      public DataRowCollection GetDataRowCollectionReadUncomited(SqlCommand sqlCommand, SqlTransaction transaction, Action logAction = null) {
         DataTable dataTable;
         DataRowCollection dataRows = null;

         dataTable = GetDataTableReadUncomited(sqlCommand, transaction, logAction);
         if (dataTable.Rows.Count > 0) {
            dataRows = dataTable.Rows;
         }

         return dataRows;
      }

      public IEnumerable<CustomDataRow> GetCustomDataRowCollectionReadUncomited(SqlCommand sqlCommand, SqlTransaction transaction, Action logAction = null) {
         return GetDataFromSqlDataReader(sqlCommand, transaction, logAction);
      }

      public object GetScalarValue(SqlCommand sqlCommand) {
         object value;

         using (SqlConnection sqlConnection = CreateSqlConnection()) {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            value = sqlCommand.ExecuteScalar();
         }

         return value;
      }

      public void ExecuteNonQuery(SqlCommand sqlCommand) {
         using (SqlConnection sqlConnection = CreateSqlConnection()) {
            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.ExecuteNonQuery();
         }
      }

      public void ExecuteNonQueryInTransaction(SqlCommand sqlCommand) {
         sqlCommand.ExecuteNonQuery();
      }

      public object GetScalarValueInTransaction(SqlCommand sqlCommand) {
         return sqlCommand.ExecuteScalar();
      }

      public DataRow GetDataRowInTransaction(SqlCommand sqlCommand) {
         DataTable dataTable;
         DataRow dataRow;

         dataTable = GetDataTableInTransaction(sqlCommand);
         dataRow = null;
         if (dataTable.Rows.Count > 0) {
            dataRow = dataTable.Rows[0];
         }

         return dataRow;
      }

      public DataRowCollection GetDataRowCollectionInTransaction(SqlCommand sqlCommand) {
         DataTable dataTable;
         DataRowCollection dataRows;

         dataTable = GetDataTableInTransaction(sqlCommand);
         dataRows = null;
         if (dataTable.Rows.Count > 0) {
            dataRows = dataTable.Rows;
         }

         return dataRows;
      }

      public DataTable GetDataTableInTransaction(SqlCommand sqlCommand) {
         DataTable dataTable = new DataTable();

         dataTable.Load(sqlCommand.ExecuteReader());

         return dataTable;
      }

      public SqlTransaction StartTransaction() {
         return StartTransaction(IsolationLevel.ReadCommitted);
      }

      public SqlTransaction StartTransaction(IsolationLevel isolationLevel) {
         SqlConnection sqlConnection = CreateSqlConnection();
         sqlConnection.Open();
         SqlTransaction transaction = sqlConnection.BeginTransaction(isolationLevel);

         return transaction;
      }

      public void EndTransaction(SqlTransaction transaction) {
         SqlConnection sqlConnection = transaction.Connection;
         transaction.Commit();
         sqlConnection.Close();
      }

      public void RollbackTransaction(SqlTransaction transaction) {
         SqlConnection sqlConnection = transaction.Connection;
         transaction.Rollback();
         sqlConnection.Close();
      }

      private SqlConnection CreateSqlConnection() {
         SqlConnection sqlConnection;
         sqlConnection = new SqlConnection(_connectionString);
         if (_enableConnectionStatistics) {
            sqlConnection.StatisticsEnabled = true;
         }

         return sqlConnection;
      }

      /// <summary>
      /// Method uses SqlDataReader to read all values from the table
      /// </summary>
      /// <param name="sqlCommand"></param>
      /// <param name="transaction"></param>
      /// <returns></returns>
      private IEnumerable<CustomDataRow> GetDataFromSqlDataReader(SqlCommand sqlCommand, SqlTransaction transaction = null, Action logAction = null) {
         using (SqlConnection sqlConnection = CreateSqlConnection()) {
            sqlConnection.Open();

            //if (CheckHelper.IsFilled(transaction))
            if (transaction != null) {
               sqlCommand.Connection = transaction.Connection;
               sqlCommand.Transaction = transaction;
            }
            else {
               sqlCommand.Connection = sqlConnection;
            }

            using (SqlDataReader reader = sqlCommand.ExecuteReader()) {
               //populate headers
               Dictionary<string, int> columnNameToIndexMapping = new Dictionary<string, int>(reader.FieldCount);
               for (int i = 0; i < reader.FieldCount; i++) {
                  columnNameToIndexMapping.Add(reader.GetName(i), i);
               }

               int count = columnNameToIndexMapping.Count;

               //populate fields
               if (reader.HasRows) {
                  while (reader.Read()) {
                     object[] values = new object[count];
                     reader.GetValues(values);
                     CustomDataRow row = new CustomDataRow(columnNameToIndexMapping, values);

                     yield return row;
                  }
               }
            }
         }
      }
   }
}