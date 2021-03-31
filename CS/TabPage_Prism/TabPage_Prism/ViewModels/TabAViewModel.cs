using System;

namespace TabPage_Prism.ViewModels {
    public class TabAViewModel : TabViewModelBase {
        protected override void OnIsActiveChanged() {
            Caption = "View A";
        }
    }
}
