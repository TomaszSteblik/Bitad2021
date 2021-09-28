using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Bitad2021.Data;
using Bitad2021.Models;
using ReactiveUI;
using Splat;

namespace Bitad2021.ViewModels
{
    public class RegisterViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment { get; } = "Registration Screen";
        public IScreen HostScreen { get; }
        private readonly IBitadService _bitadService;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public ICommand RegisterCommand { get; set; }
        
        public RegisterViewModel(IBitadService bitadService = null, IScreen screen = null)
        {
            _bitadService = bitadService ?? Locator.Current.GetService<IBitadService>();
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            
            RegisterCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var res = await _bitadService.Register(Email,FirstName,LastName,Username,Password);
                
                if(res is null)
                    //TODO:HANDLE ERROR
                    return;
                Debug.WriteLine(res.Name);
                HostScreen.Router.NavigateBack.Execute();
            });
        }
    }
}