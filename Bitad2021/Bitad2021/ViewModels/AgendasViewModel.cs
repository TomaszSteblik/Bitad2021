using System.Threading.Tasks;
using Bitad2021.Data;
using Bitad2021.Models;
using DynamicData.Binding;
using ReactiveUI;
using Splat;

namespace Bitad2021.ViewModels
{
    public class AgendasViewModel : ReactiveObject
    {
        private readonly IBitadService _bitadService;

        public IObservableCollection<Agenda> Agendas { get; set; }
        
        public AgendasViewModel(IBitadService bitadService)
        {
            _bitadService = bitadService ?? Locator.Current.GetService<IBitadService>();
            LoadAgendas();
        }

        private async Task LoadAgendas()
        {
            Agendas = new ObservableCollectionExtended<Agenda>(await _bitadService.GetAllAgendas());
        }
    }
}