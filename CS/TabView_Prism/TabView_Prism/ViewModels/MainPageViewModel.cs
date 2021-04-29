using Prism.Navigation;
using TabView_Prism.Views;
using Xamarin.Forms;

namespace TabView_Prism.ViewModels {
    public class MainPageViewModel {
        public MainPageViewModel(INavigationService navigationService) {
            GoToButtonCommand = new Command(() => navigationService.NavigateAsync(nameof(TabViewPage)));
        }
        
        public Command GoToButtonCommand { get; private set; }
    }
}
