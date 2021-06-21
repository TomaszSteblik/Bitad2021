using System;
using System.Diagnostics;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using Bitad2021.Data;
using Bitad2021.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace Bitad2021.ViewModels
{
    public class LoginViewModel : ReactiveObject, IRoutableViewModel
    {
        private readonly IBitadService _bitadService;

        public string UrlPathSegment { get; } = "Login Screen";
        public IScreen HostScreen { get; }
        
        [Reactive]
        public string Username { get; set; }
        [Reactive]
        public string Password { get; set; }
        public ReactiveCommand<Unit,Unit> LoginCommand { get; set; }
        
        public ICommand RegisterNavigationCommand { get; set; }
        public ICommand TabbedNavigationCommand { get; set; }
        
        public LoginViewModel(IBitadService bitadService = null, IScreen screen = null)
        {
            _bitadService = bitadService ?? Locator.Current.GetService<IBitadService>();
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            
            LoginCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                
                var res = await _bitadService.Login(Username, Password);
                if(res is null)
                    //TODO:HANDLE ERROR
                    return;
                Debug.WriteLine(res.Name);
                TabbedNavigationCommand.Execute(null);
                
            });
            LoginCommand.ThrownExceptions.Subscribe(ex => Debug.WriteLine(ex.Message));
            
            RegisterNavigationCommand = ReactiveCommand.CreateFromObservable(() =>
            {
                return HostScreen.Router.Navigate.Execute(new RegisterViewModel());
            });

            TabbedNavigationCommand = ReactiveCommand.CreateFromObservable(() =>
            {
                return HostScreen.Router.NavigateAndReset.Execute(new TabbedViewModel());
            });

        }

        
    }
}