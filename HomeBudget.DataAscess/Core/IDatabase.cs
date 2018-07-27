using System;
using System.Data;
using System.Data.SqlClient;

namespace HomeBudget.DataAccess.Core {

   public interface IDatabase {

      DataTable GetDataTable(SqlCommand sqlCommand, Action logAction = null);
   }
}