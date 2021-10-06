using System.Diagnostics;
using System.Windows.Input;
using Bitad2021.Data;
using Bitad2021.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using Xamarin.Forms;

namespace Bitad2021.ViewModels
{
    public class TabbedViewModel : ReactiveObject, IRoutableViewModel
    {
        private readonly IBitadService _bitadService;
        public string UrlPathSegment { get; } = "Bitad 2021";
        public IScreen HostScreen { get; }

        public AgendasViewModel AgendasViewModel { get; set; }
        public SettingsViewModel SettingsViewModel { get; set; }
        public QrScannerViewModel QrScannerViewModel { get; set; }


        public TabbedViewModel(User user, IBitadService bitadService = null, IScreen screen = null)
        {
            _bitadService = bitadService ?? Locator.Current.GetService<IBitadService>();
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();

            AgendasViewModel = new AgendasViewModel();
            SettingsViewModel = new SettingsViewModel(user);
            QrScannerViewModel = new QrScannerViewModel();

        }
    }
}