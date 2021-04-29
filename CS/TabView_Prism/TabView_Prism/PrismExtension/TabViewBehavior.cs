using System;
using System.ComponentModel;
using DevExpress.XamarinForms.Navigation;
using Prism;
using Prism.Behaviors;
using Prism.Common;
using Xamarin.Forms;

namespace TabView_Prism.PrismExtension {
    public class TabViewBehavior : BehaviorBase<ContentPage> {
        object lastSelectedPage;
        
        protected override void OnAttachedTo(ContentPage bindable) {
            if(!IsTabViewBasedPage(bindable)) {
                return;
            }
            
            bindable.Appearing += TabViewOnAppearing;
            bindable.Disappearing += TabViewOnDisappearing;
            TabView tabView = GetTabView(bindable);
            tabView.PropertyChanged += TabViewOnPropertyChanged;
            
            base.OnAttachedTo(bindable);
        }
        
        protected override void OnDetachingFrom(ContentPage bindable) {
            if(!IsTabViewBasedPage(bindable)) {
                return;
            }
            
            bindable.Appearing -= TabViewOnAppearing;
            bindable.Disappearing -= TabViewOnDisappearing;
            TabView tabView = GetTabView(bindable);
            tabView.PropertyChanged -= TabViewOnPropertyChanged;
            
            base.OnDetachingFrom(bindable);
        }
        
        void TabViewOnAppearing(object sender, EventArgs e) {
            if (lastSelectedPage == null) {
                lastSelectedPage = GetSelectedPage();
            }
            
            SetIsActive(lastSelectedPage, true);
        }
        
        void TabViewOnDisappearing(object sender, EventArgs e) {
            SetIsActive(AssociatedObject, false);
            SetIsActive(GetTabView(AssociatedObject), false);
        }
        
        void TabViewOnPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (string.Equals(e.PropertyName, nameof(TabView.SelectedItem))) {
                if (lastSelectedPage != null) {
                    SetIsActive(lastSelectedPage, false);
                }

                lastSelectedPage = GetSelectedPage();
                SetIsActive(lastSelectedPage, true);
            }
        }
        
        void SetIsActive(object view, bool isActive) {
            var pageToSetIsActive = view is NavigationPage page 
                ? page.CurrentPage 
                : view is TabViewItem tabItem 
                    ? tabItem.Content 
                    : view;

            PageUtilities.InvokeViewAndViewModelAction<IActiveAware>(pageToSetIsActive, activeAware => activeAware.IsActive = isActive);
        }

        
        TabViewItem GetSelectedPage() {
            TabView tabView = GetTabView(AssociatedObject);
            
            if (tabView.SelectedItemIndex >= 0 && tabView.SelectedItemIndex < tabView.Items.Count) {
                return tabView.Items[tabView.SelectedItemIndex];
            }
            
            return null;
        }
        
        bool IsTabViewBasedPage(ContentPage page) {
            return GetTabView(page) != null;
        }
        
        TabView GetTabView(ContentPage page) {
            return page.Content as TabView;
        }
    }
}
