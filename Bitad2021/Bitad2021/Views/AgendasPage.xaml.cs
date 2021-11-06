using Bitad2021.ViewModels;
using ReactiveUI.XamForms;
using Xamarin.Forms.Xaml;

namespace Bitad2021.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgendasPage : ReactiveTabbedPage<AgendasViewModel>
    {
        public AgendasPage()
        {
            InitializeComponent();
        }
    }
}