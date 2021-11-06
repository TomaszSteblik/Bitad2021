using Bitad2021.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace Bitad2021.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ReactiveContentPage<LoginViewModel>
    {
        public LoginPage()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                var hasPassword = Preferences.ContainsKey("password");
                var hasUsername = Preferences.ContainsKey("username");
                if (hasPassword && hasUsername)
                {
                    ViewModel.Password = Preferences.Get("password", "");
                    ViewModel.Username = Preferences.Get("username", "");

                    ViewModel.LoginCommand.Execute();
                }
            });
        }
    }
}