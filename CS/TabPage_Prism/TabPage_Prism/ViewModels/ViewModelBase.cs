using System;
using Prism;
using Prism.Mvvm;

namespace TabPage_Prism.ViewModels {
    public class ViewModelBase : BindableBase, IActiveAware {
        bool isActive;

        public bool IsActive {
            get => isActive;
            set {
                if (value != isActive) {
                    isActive = value;
                    IsActiveChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler IsActiveChanged;
    }
}
