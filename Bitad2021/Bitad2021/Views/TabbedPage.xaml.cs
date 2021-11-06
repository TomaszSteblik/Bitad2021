using Bitad2021.ViewModels;
using ReactiveUI.XamForms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace Bitad2021.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedView : ReactiveTabbedPage<TabbedViewModel>
    {
        public TabbedView()
        {
            InitializeComponent();
            TabbedPage.SetIsSwipePagingEnabled(this, false);
        }
    }
}