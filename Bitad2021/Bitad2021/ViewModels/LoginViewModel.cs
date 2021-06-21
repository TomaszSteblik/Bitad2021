using System.Diagnostics;
using System.Reactive;
using System.Windows.Input;
using Bitad2021.Data;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace Bitad2021.ViewModels
{
    public class LoginViewModel : ReactiveObject
    {
        private readonly IBitadService _bitadService;

        [Reactive]
        public string Username { get; set; }
        [Reactive]
        public string Password { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        
        public LoginViewModel(IBitadService bitadService = null)
        {
            _bitadService = bitadService ?? Locator.Current.GetService<IBitadService>();
            LoginCommand = ReactiveCommand.Create(() =>
            {
                Debug.WriteLine("Login clicked");
                Debug.WriteLine(Username);
                Debug.WriteLine(Password);
            });
            RegisterCommand = ReactiveCommand.Create(() =>
            {
                Debug.WriteLine("register button clicked");
            });
        }
    }
}