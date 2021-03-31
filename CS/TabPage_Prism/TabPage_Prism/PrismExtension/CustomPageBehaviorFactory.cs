using DevExpress.XamarinForms.Navigation;
using Prism.Behaviors;
using Xamarin.Forms;

namespace TabPage_Prism.PrismExtension {
    public class CustomPageBehaviorFactory : PageBehaviorFactory {
        protected override void ApplyPageBehaviors(Page page) {
            if (page is TabPage) {
                page.Behaviors.Add(new TabPageBehavior());
            }
            
            base.ApplyPageBehaviors(page);
        }
    }

}
