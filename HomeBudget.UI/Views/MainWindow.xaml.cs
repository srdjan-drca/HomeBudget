using MahApps.Metro.Controls;

namespace HomeBudget.Views {

   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : MetroWindow {

      public MainWindow() {
         InitializeComponent();

         HamburgerMenuControl.SelectedIndex = 0;
      }

      private void HamburgerMenuControl_OnItemClick(object sender, ItemClickEventArgs e) {
         this.HamburgerMenuControl.Content = e.ClickedItem;
         this.HamburgerMenuControl.IsPaneOpen = false;
      }
   }
}