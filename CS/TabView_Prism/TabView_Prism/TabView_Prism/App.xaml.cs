using System;
using Prism.Behaviors;
using Prism.Ioc;
using Prism.Navigation;
using Prism.Unity;
using TabView_Prism.PrismExtension;
using TabView_Prism.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace TabView_Prism {
    public partial class App : PrismApplication {
        protected override void RegisterTypes(IContainerRegistry containerRegistry) {
            containerRegistry.RegisterScoped<INavigationService, CustomNavigationService>();
            
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<TabViewPage>();
        }
		
        protected override void OnInitialized() {
            InitializeComponent();

            NavigationService.NavigateAsync(nameof(MainPage));
        }
    }
}
