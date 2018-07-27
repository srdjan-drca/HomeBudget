using HomeBudget.Configuration;
using HomeBudget.Tools.SimpleIoc;

namespace HomeBudget.ViewModels {

   public class ViewModelLocator {

      private readonly IContainer _iocContainer;

      public ViewModelLocator() {
         _iocContainer = BootStrapper.GetIocContainer();
      }

      public AllCostsViewModel AllCostsViewModel => _iocContainer.Resolve<AllCostsViewModel>();

      public NewCostsViewModel NewCostsViewModel => _iocContainer.Resolve<NewCostsViewModel>();

      public SettingsViewModel SettingsViewModel => _iocContainer.Resolve<SettingsViewModel>();
   }
}