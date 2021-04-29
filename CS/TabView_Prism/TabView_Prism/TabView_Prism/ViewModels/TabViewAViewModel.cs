using System;
using Prism.Navigation;
using Xamarin.Forms;

namespace TabView_Prism.ViewModels {
    public class TabViewAViewModel : TabViewModelBase {
        protected override void OnIsActiveChanged() {
            Caption = "View A";
        }
    }
}
