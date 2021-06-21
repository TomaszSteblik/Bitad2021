using Bitad2021.Data;
using ReactiveUI;
using Splat;

namespace Bitad2021.ViewModels
{
    public class TabbedViewModel : ReactiveObject, IRoutableViewModel
    {
        private readonly IBitadService _bitadService;
        public string UrlPathSegment { get; } = "Bitad 2021";
        public IScreen HostScreen { get; }

        public TabbedViewModel(IBitadService bitadService = null, IScreen screen = null)
        {
            _bitadService = bitadService ?? Locator.Current.GetService<IBitadService>();
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
        }
    }
}