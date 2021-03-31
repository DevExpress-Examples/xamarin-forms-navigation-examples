namespace TabPage_Prism.ViewModels {
    public class TabBViewModel : TabViewModelBase {

        protected override void OnIsActiveChanged() {
            Caption = "View B";
        }
    }
}
