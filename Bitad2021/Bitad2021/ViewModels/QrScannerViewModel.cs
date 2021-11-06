using System;
using System.Net;
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
using ZXing;

namespace Bitad2021.ViewModels
{
    public class QrScannerViewModel : ReactiveObject
    {
        private readonly IBitadService _bitadService;
        public IScreen _hostScreen;


        public QrScannerViewModel(SettingsViewModel settings, IBitadService bitadService = null,
            IScreen hostScreen = null)
        {
            IsScanning = true;
            IsAnalyzing = true;
            IsAnimationVisible = true;

            _bitadService = bitadService ?? Locator.Current.GetService<IBitadService>();
            _hostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();

            LogoutCommand = ReactiveCommand.Create(() =>
            {
                Preferences.Remove("password");
                Preferences.Remove("username");
                LoginNavigationCommand.Execute(null);
            });


            LoginNavigationCommand = ReactiveCommand.CreateFromObservable(() =>
                _hostScreen.Router.NavigateAndReset.Execute(new LoginViewModel()));

            ScanResultCommand = new Command(async () =>
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Application.Current.MainPage.DisplayToastAsync("Błąd połączenia");
                    await Task.Delay(10000);
                    return;
                }

                IsAnalyzing = false;

                var response = await _bitadService.RedeemQrCode(Result.Text);

                //If i delete animations later, i can just use toast outside of mainthread
                Device.BeginInvokeOnMainThread(async () =>
                {
                    OnQrResponseReceived?.Invoke(this, response);
                    if (response.Item1 is not null)
                        settings.CurrentScore += response.Item1.Points;
                });

                await Task.Delay(5000);
                IsAnalyzing = true;

                if (response.code == HttpStatusCode.Unauthorized)
                    LogoutCommand.Execute();
            });
        }

        public ICommand ScanResultCommand { get; set; }
        public ICommand LoginNavigationCommand { get; set; }

        public ReactiveCommand<Unit, Unit> LogoutCommand { get; set; }

        [Reactive] public Result Result { get; set; }

        [Reactive] public bool IsAnalyzing { get; set; }

        [Reactive] public bool IsScanning { get; set; }

        [Reactive] public bool IsAnimationVisible { get; set; }

        public event EventHandler<(QrCodeResponse, HttpStatusCode)> OnQrResponseReceived;
    }
}