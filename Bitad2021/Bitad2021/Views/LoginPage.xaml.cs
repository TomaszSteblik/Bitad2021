using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bitad2021.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

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
                bool hasPassword = Preferences.ContainsKey("password");
                bool hasUsername = Preferences.ContainsKey("username");
                if (hasPassword && hasUsername)
                {
                    ViewModel.Password =  Preferences.Get("password","");
                    ViewModel.Username =  Preferences.Get("username","");

                    ViewModel.LoginCommand.Execute();
                }
            });
        }
    }
}