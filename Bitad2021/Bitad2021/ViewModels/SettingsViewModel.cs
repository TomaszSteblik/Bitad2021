using System.Diagnostics;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using Bitad2021.Data;
using Bitad2021.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Bitad2021.ViewModels
{
    public class SettingsViewModel : ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment => "Settings page";
        public IScreen HostScreen { get; }
        public readonly IBitadService _bitadService;

        public ICommand LoginNavigationCommand { get; set; }

        public ReactiveCommand<Unit,Unit> LogoutCommand { get; set; }


        [Reactive]
        public User User { get; set; }
        
        [Reactive]
        public string AttendanceCode { get; set; }
        [Reactive]
        public int CurrentScore { get; set; }
        [Reactive]
        public bool IsWorkshopCodeVisible { get; set; }

        public SettingsViewModel(ref User user, IBitadService bitadService = null, IScreen hostScreen = null)
        {
            _bitadService = bitadService ?? Locator.Current.GetService<IBitadService>();
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();

            LogoutCommand = ReactiveCommand.Create(() =>
            {
                Preferences.Remove("password");
                Preferences.Remove("username");
                LoginNavigationCommand.Execute(null);
            });
            
            
            LoginNavigationCommand = ReactiveCommand.CreateFromObservable(() => 
                HostScreen.Router.NavigateAndReset.Execute(new LoginViewModel()));
            
            User = user;
            CurrentScore = user.CurrentScore;
            AttendanceCode = User.AttendanceCode;
            IsWorkshopCodeVisible = User.WorkshopAttendanceCode is not null;

        }
    }
}