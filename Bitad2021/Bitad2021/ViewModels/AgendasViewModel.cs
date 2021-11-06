using Bitad2021.Data;
using Bitad2021.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace Bitad2021.ViewModels
{
    public class AgendasViewModel : ReactiveObject
    {
        private readonly IBitadService _bitadService;

        public AgendasViewModel(ref User user, IBitadService bitadService = null)
        {
            _bitadService = bitadService ?? Locator.Current.GetService<IBitadService>();

            LecturesViewModel = new LecturesViewModel();

            WorkshopsViewModel = new WorkshopsViewModel(ref user);
        }

        [Reactive] public WorkshopsViewModel WorkshopsViewModel { get; set; }

        [Reactive] public LecturesViewModel LecturesViewModel { get; set; }
    }
}