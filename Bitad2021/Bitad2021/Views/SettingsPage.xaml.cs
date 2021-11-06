using System.Threading.Tasks;
using Bitad2021.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bitad2021.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ReactiveContentPage<SettingsViewModel>
    {
        public SettingsPage()
        {
            InitializeComponent();
            this.WhenActivated(async disposable =>
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Application.Current.MainPage.DisplayToastAsync("Błąd połączenia");
                    await Task.Delay(10000);
                    return;
                }

                ViewModel.User = await ViewModel._bitadService.GetUser();
                ViewModel.IsWorkshopVisible = ViewModel.User.Workshop is not null;
            });
        }
    }
}