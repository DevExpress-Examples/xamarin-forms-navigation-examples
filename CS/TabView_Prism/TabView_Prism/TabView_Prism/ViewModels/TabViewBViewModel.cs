using System;
using Prism.Navigation;
using Xamarin.Forms;

namespace TabView_Prism.ViewModels {
    public class TabViewBViewModel : TabViewModelBase {
        public TabViewBViewModel(INavigationService navigationService) {
            GoBackCommand = new Command(() => navigationService.GoBackAsync());
        }
        protected override void OnIsActiveChanged() {
            Caption = "View B";
        }
        
        public Command GoBackCommand { get; private set; }
    }
}
