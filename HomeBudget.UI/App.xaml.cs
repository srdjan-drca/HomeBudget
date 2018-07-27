using System.Windows;
using HomeBudget.Configuration;
using HomeBudget.Report.Helpers;

namespace HomeBudget {

   /// <summary>
   /// Interaction logic for App.xaml
   /// </summary>
   public partial class App : Application {

      protected override void OnStartup(StartupEventArgs e) {
         PdfHelper.InitializeIText();
         BootStrapper.InitializeIocContainer();
      }
   }
}