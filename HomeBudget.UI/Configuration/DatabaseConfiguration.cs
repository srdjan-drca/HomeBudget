using System;
using System.Configuration;
using HomeBudget.DataAccess.Configuration;

namespace HomeBudget.Configuration {

   public class DatabaseConfiguration : IDatabaseConfiguration {
      private string _connectionString;

      private bool _enableConnectionStatistics;

      public string ConnectionString {
         get {
            _connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

            return _connectionString;
         }

         set {
            _connectionString = value;
         }
      }

      public bool EnableConnectionStatistics {
         get {
            _enableConnectionStatistics = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableConnectionStatistics"]);

            return _enableConnectionStatistics;
         }

         set {
            _enableConnectionStatistics = value;
         }
      }
   }
}