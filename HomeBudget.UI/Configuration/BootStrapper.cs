using HomeBudget.DataAccess.Configuration;
using HomeBudget.DataAccess.Core;
using HomeBudget.DataAccess.Repositories.Implementation;
using HomeBudget.DataAccess.Repositories.Interfaces;
using HomeBudget.Tools.SimpleIoc;
using HomeBudget.ViewModels;

namespace HomeBudget.Configuration {

   public static class BootStrapper {
      private static SimpleIocContainer _iocContainer;

      public static void InitializeIocContainer() {
         _iocContainer = new SimpleIocContainer();

         _iocContainer.Register<ICostRepository, CostRepository>();
         _iocContainer.Register<ICriteriaRepository, CriteriaRepository>();
         _iocContainer.Register<ICriteriaValueRepository, CriteriaValueRepository>();
         _iocContainer.Register<ILabelRepository, LabelRepository>();
         _iocContainer.Register<ILabelTranslationRepository, LabelTranslationRepository>();
         _iocContainer.Register<ISqlServerDatabase, SqlServerDatabase>();
         _iocContainer.Register<IDatabaseConfiguration, DatabaseConfiguration>();
         _iocContainer.Register<NewCostsViewModel, NewCostsViewModel>();
         _iocContainer.Register<AllCostsViewModel, AllCostsViewModel>();
         _iocContainer.Register<SettingsViewModel, SettingsViewModel>();
         _iocContainer.Register<BaseViewModel, BaseViewModel>();
      }

      public static IContainer GetIocContainer() {
         return _iocContainer;
      }
   }
}