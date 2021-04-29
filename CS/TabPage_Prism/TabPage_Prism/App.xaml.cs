using System;
using Prism.Behaviors;
using Prism.Ioc;
using Prism.Navigation;
using Prism.Unity;
using TabPage_Prism.PrismExtension;
using TabPage_Prism.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabPage_Prism {
	public partial class App : PrismApplication {
		protected override void RegisterTypes(IContainerRegistry containerRegistry) {
			containerRegistry.RegisterScoped<INavigationService, CustomNavigationService>();
			containerRegistry.RegisterSingleton<IPageBehaviorFactory, CustomPageBehaviorFactory>();
			
			containerRegistry.RegisterForNavigation<MainPage>();
			containerRegistry.RegisterForNavigation<TabbedForm>();
			containerRegistry.RegisterForNavigation<TabA>();
			containerRegistry.RegisterForNavigation<TabB>();
		}
		
		protected override void OnInitialized() {
			InitializeComponent();

			NavigationService.NavigateAsync(nameof(MainPage));
		}
	}
}
