using System;
using System.ComponentModel;
using DevExpress.XamarinForms.Navigation;
using Prism;
using Prism.Behaviors;
using Prism.Common;
using Xamarin.Forms;

namespace TabPage_Prism.PrismExtension {
    public class TabPageBehavior : BehaviorBase<TabPage> {
        object lastSelectedPage;
        
        protected override void OnAttachedTo(TabPage bindable) {
            bindable.Appearing += TabPageOnAppearing;
            bindable.Disappearing += TabPageOnDisappearing;
            bindable.PropertyChanged += TabPageOnPropertyChanged;
            
            base.OnAttachedTo(bindable);
        }
        
        protected override void OnDetachingFrom(TabPage bindable) {
            bindable.Appearing -= TabPageOnAppearing;
            bindable.Disappearing -= TabPageOnDisappearing;
            bindable.PropertyChanged -= TabPageOnPropertyChanged;
            
            base.OnDetachingFrom(bindable);
        }

        void TabPageOnAppearing(object sender, EventArgs e) {
            if (lastSelectedPage == null) {
                lastSelectedPage = GetSelectedPage();
            }
            
            SetIsActive(lastSelectedPage, true);
        }
        
        void TabPageOnDisappearing(object sender, EventArgs e) {
            SetIsActive(AssociatedObject, false);
        }
        
        void TabPageOnPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (string.Equals(e.PropertyName, nameof(TabPage.SelectedItem))) {
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
                : view is TabPageItem tabItem 
                    ? tabItem.Content 
                    : view;

            PageUtilities.InvokeViewAndViewModelAction<IActiveAware>(pageToSetIsActive, activeAware => activeAware.IsActive = isActive);
        }

        TabPageItem GetSelectedPage() {
            if (AssociatedObject.SelectedItemIndex >= 0 && AssociatedObject.SelectedItemIndex < AssociatedObject.Items.Count) {
                return AssociatedObject.Items[AssociatedObject.SelectedItemIndex];
            }
            
            return null;
        }
    }
}
