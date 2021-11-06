using Bitad2021.Data;
using Bitad2021.Models;
using ReactiveUI;
using Splat;

namespace Bitad2021.ViewModels
{
    public class TabbedViewModel : ReactiveObject, IRoutableViewModel
    {
        private readonly IBitadService _bitadService;


        public TabbedViewModel(User user, IBitadService bitadService = null, IScreen screen = null)
        {
            _bitadService = bitadService ?? Locator.Current.GetService<IBitadService>();
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();

            AgendasViewModel = new AgendasViewModel(ref user);
            SettingsViewModel = new SettingsViewModel(ref user);
            QrScannerViewModel = new QrScannerViewModel(SettingsViewModel);
        }

        public AgendasViewModel AgendasViewModel { get; set; }
        public SettingsViewModel SettingsViewModel { get; set; }
        public QrScannerViewModel QrScannerViewModel { get; set; }
        public string UrlPathSegment { get; } = "Bitad 2021";
        public IScreen HostScreen { get; }
    }
}