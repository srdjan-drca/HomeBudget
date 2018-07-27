namespace HomeBudget.DataAccess.Configuration {

   public interface IDatabaseConfiguration {
      string ConnectionString { get; set; }

      bool EnableConnectionStatistics { get; set; }
   }
}