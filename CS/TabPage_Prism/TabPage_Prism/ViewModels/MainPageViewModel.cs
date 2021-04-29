using Prism.Navigation;
using TabPage_Prism.Views;
using Xamarin.Forms;

namespace TabPage_Prism.ViewModels {
    public class MainPageViewModel {
        public MainPageViewModel(INavigationService navigationService) {
            GoToButtonCommand = new Command(() => navigationService.NavigateAsync(nameof(TabbedForm) + "?selectedTab=TabB"));
        }
        
        public Command GoToButtonCommand { get; private set; }
    }
}
