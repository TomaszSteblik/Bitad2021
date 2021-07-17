using System.Windows.Input;
using Bitad2021.Data;
using ReactiveUI;
using Splat;

namespace Bitad2021.ViewModels
{
    public class SettingsViewModel : ReactiveObject
    {
        private readonly IBitadService _bitadService;
        
        public SettingsViewModel(IBitadService bitadService = null)
        {
            _bitadService = bitadService ?? Locator.Current.GetService<IBitadService>();
            
            
        }
    }
}