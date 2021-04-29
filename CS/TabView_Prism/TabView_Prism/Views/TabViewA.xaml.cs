using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabView_Prism.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabViewA : ContentView {
        public TabViewA() {
            InitializeComponent();
        }

        protected override void OnParentSet() {
            base.OnParentSet();
        }
    }
}

