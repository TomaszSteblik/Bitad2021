using System;
using System.Diagnostics;
using System.Net;
using System.Reactive;
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
    public class LoginViewModel : ReactiveObject, IRoutableViewModel
    {
        private readonly IBitadService _bitadService;

        public LoginViewModel(IBitadService bitadService = null, IScreen screen = null)
        {
            _bitadService = bitadService ?? Locator.Current.GetService<IBitadService>();
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();

            var user = new User();


            LoginCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Application.Current.MainPage.DisplayToastAsync("Błąd połączenia");
                    return;
                }

                var res = await _bitadService.Login(Username, Password);
                if (res.user is null)
                {
                    switch (res.code)
                    {
                        case HttpStatusCode.Forbidden:
                            //nieaktywne konto
                            await Application.Current.MainPage.DisplaySnackBarAsync("Konto nieaktywowane",
                                "Wyślij email ponownie",
                                async () =>
                                {
                                    if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                                    {
                                        await Application.Current.MainPage.DisplayToastAsync("Błąd połączenia");
                                        return;
                                    }

                                    var reqResponse = await _bitadService.RequestActivationResend(Username);

                                    await Application.Current.MainPage.DisplayToastAsync(reqResponse
                                        ? "Sprawdź pocztę email"
                                        : "Coś poszło nie tak...");
                                });
                            break;
                        case HttpStatusCode.NotFound:
                            //bledne haslo
                            await Application.Current.MainPage.DisplaySnackBarAsync("Błędne dane logowania",
                                "Zresetuj hasło",
                                async () =>
                                {
                                    if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                                    {
                                        await Application.Current.MainPage.DisplayToastAsync("Błąd połączenia");
                                        return;
                                    }

                                    var reqResponse = await _bitadService.IssuePasswordReset(Username);

                                    await Application.Current.MainPage.DisplayToastAsync(reqResponse
                                        ? "Sprawdź pocztę email"
                                        : "Coś poszło nie tak...");
                                });
                            break;
                        default:
                            await Application.Current.MainPage.DisplayToastAsync("Nieznany błąd");
                            break;
                    }

                    return;
                }

                user = res.user;

                Preferences.Set("password", Password);
                Preferences.Set("username", Username);

                TabbedNavigationCommand.Execute(null);
            });
            LoginCommand.ThrownExceptions.Subscribe(ex => Debug.WriteLine(ex.Message));

            TabbedNavigationCommand = ReactiveCommand.CreateFromObservable(() =>
            {
                return HostScreen.Router.NavigateAndReset.Execute(new TabbedViewModel(user));
            });

            TapLinkCommand = ReactiveCommand.CreateFromTask(async (string url) => { await Launcher.OpenAsync(url); });
        }

        [Reactive] public bool IsVisible { get; set; }


        [Reactive] public string Username { get; set; }

        [Reactive] public string Password { get; set; }

        public ReactiveCommand<Unit, Unit> LoginCommand { get; set; }
        public ReactiveCommand<string, Unit> TapLinkCommand { get; set; }

        public ICommand TabbedNavigationCommand { get; set; }

        public string UrlPathSegment { get; } = "Login Screen";
        public IScreen HostScreen { get; }

        //TODO: Dodać różne rozmiary zdjecia loga
    }
}