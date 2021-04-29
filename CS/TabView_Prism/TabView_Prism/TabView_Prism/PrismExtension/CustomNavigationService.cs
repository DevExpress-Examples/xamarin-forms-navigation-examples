using DevExpress.XamarinForms.Navigation;
using Prism.Behaviors;
using Prism.Common;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Forms;

namespace TabView_Prism.PrismExtension {
    public class CustomNavigationService :  PageNavigationService {
        public CustomNavigationService(IContainerProvider container, IApplicationProvider applicationProvider, IPageBehaviorFactory pageBehaviorFactory) 
            : base(container, applicationProvider, pageBehaviorFactory) {
            
        }

        protected override Page CreatePageFromSegment(string segment) {
            var page = base.CreatePageFromSegment(segment);
            if (page is ContentPage contentPage && contentPage.Content is TabView tabView) {
                ConfigureTabView(contentPage, tabView);
            }

            return page;
        }

        void ConfigureTabView(ContentPage owningPage, TabView tabView) {
            foreach (TabViewItem tabViewItem in tabView.Items) {
                PageUtilities.SetAutowireViewModel(tabViewItem.Content);
            }
            
            owningPage.Behaviors.Add(new TabViewBehavior());
        }
    }

}
