using System.ComponentModel;
using PropertyChanged;

namespace HomeBudget.ViewModels {

   [AddINotifyPropertyChangedInterface]
   public class BaseViewModel : INotifyPropertyChanged {

      public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
   }
}