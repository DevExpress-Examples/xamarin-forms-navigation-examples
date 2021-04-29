using System;

namespace TabView_Prism.ViewModels {
    public abstract class TabViewModelBase : ViewModelBase {
        string caption;

        static void OnIsActiveChanged(object sender, EventArgs e) {
            ((TabViewModelBase)sender).OnIsActiveChanged();
        }
        
        protected TabViewModelBase() {
            IsActiveChanged += OnIsActiveChanged;
        }

        public string Caption {
            get => caption;
            set => SetProperty(ref caption, value);
        }

        protected abstract void OnIsActiveChanged();
    }
}
