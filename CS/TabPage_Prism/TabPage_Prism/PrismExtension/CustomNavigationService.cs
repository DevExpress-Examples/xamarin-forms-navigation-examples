using System.Linq;
using DevExpress.XamarinForms.Navigation;
using Prism.Behaviors;
using Prism.Common;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Forms;

namespace TabPage_Prism.PrismExtension {
    public class CustomNavigationService : PageNavigationService {
        public CustomNavigationService(IContainerProvider container, IApplicationProvider applicationProvider, IPageBehaviorFactory pageBehaviorFactory) 
            : base(container, applicationProvider, pageBehaviorFactory) {

        }

        protected override Page CreatePageFromSegment(string segment) {
            var page = base.CreatePageFromSegment(segment);
            if (page is TabPage tabPage) {
                ConfigureTabPage(tabPage, segment);
            }
            
            return page;
        }

        void ConfigureTabPage(TabPage tabbedPage, string segment) {
            ApplyPageBehaviors(tabbedPage);

            var parameters = UriParsingHelper.GetSegmentParameters(segment);

            var tabsToCreate = parameters.GetValues<string>(KnownNavigationParameters.CreateTab);
            if (tabsToCreate.Any()) {
                foreach (var tabToCreate in tabsToCreate) {
                    var tabSegments = tabToCreate.Split('|');
                    if (tabSegments.Length > 1) {
                        if (CreatePageFromSegment(tabSegments[0]) is NavigationPage navigationPage) {
                            var navigationPageChild = CreatePageFromSegment(tabSegments[1]);
                            navigationPage.PushAsync(navigationPageChild);
                            
                            if (navigationPage.Navigation.NavigationStack.Count > 1)
                                navigationPage.Navigation.RemovePage(navigationPage.Navigation.NavigationStack[0]);
                            
                            navigationPage.Title = navigationPageChild.Title;
                            navigationPage.IconImageSource = navigationPageChild.IconImageSource;

                            var tabPageItem = new TabPageItem {
                                Content = navigationPage
                            };
                            tabbedPage.Items.Add(tabPageItem);
                        }
                    }
                    else {
                        var tab = CreatePageFromSegment(tabToCreate);
                        var tabPageItem = new TabPageItem {
                            Content = tab
                        };
                        tabbedPage.Items.Add(tabPageItem);
                    }
                }
            }

            TabbedPageSelectTab(tabbedPage, parameters);
        }

        static void TabbedPageSelectTab(TabPage tabbedPage, INavigationParameters parameters) {
            var selectedTab = parameters?.GetValue<string>(KnownNavigationParameters.SelectedTab);
            if (string.IsNullOrWhiteSpace(selectedTab)) {
                return;
            }
            
            var selectedTabType = PageNavigationRegistry.GetPageType(UriParsingHelper.GetSegmentName(selectedTab));

            var childFound = false;
            foreach (var child in tabbedPage.Items) {
                if (!childFound && child.Content.GetType() == selectedTabType) {
                    tabbedPage.SelectedItemIndex = tabbedPage.Items.IndexOf(child);
                    childFound = true;
                }

                if (child.Content is NavigationPage navigationPage) {
                    if (!childFound && navigationPage.CurrentPage.GetType() == selectedTabType) {
                        tabbedPage.SelectedItemIndex = tabbedPage.Items.IndexOf(child);
                        childFound = true;
                    }
                }
            }
        }
        
        void ApplyPageBehaviors(TabPage tabbedPage) {
            foreach (var child in tabbedPage.Items) {
                PageUtilities.SetAutowireViewModel(child.Content);
                _pageBehaviorFactory.ApplyPageBehaviors(child.Content);
                if (child.Content is NavigationPage navPage) {
                    PageUtilities.SetAutowireViewModel(navPage.CurrentPage);
                    _pageBehaviorFactory.ApplyPageBehaviors(navPage.CurrentPage);
                }
            }
        }
    }
}
