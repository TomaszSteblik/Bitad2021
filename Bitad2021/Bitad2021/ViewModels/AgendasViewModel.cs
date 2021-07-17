using System;
using System.Diagnostics;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using Bitad2021.Data;
using Bitad2021.Models;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace Bitad2021.ViewModels
{
    public class AgendasViewModel : ReactiveObject
    {
        private readonly IBitadService _bitadService;

        [Reactive]
        public IObservableCollection<Agenda> Agendas { get; set; }
        [Reactive]
        public string Count { get; set; } = "dsa";
        public ReactiveCommand<Unit,Unit> Command { get; set; }
        
        public AgendasViewModel(IBitadService bitadService = null)
        {
            _bitadService = bitadService ?? Locator.Current.GetService<IBitadService>();
            Agendas = new ObservableCollectionExtended<Agenda>();
            Command = ReactiveCommand.CreateFromTask(async () =>
            {
                Agendas.AddRange(await _bitadService.GetAllAgendas());
                Count = Agendas.Count.ToString();
            });
            Command.ThrownExceptions.Subscribe(ex => Debug.WriteLine(((Exception) ex).Message));
            Command.Execute();
        }
    }
}