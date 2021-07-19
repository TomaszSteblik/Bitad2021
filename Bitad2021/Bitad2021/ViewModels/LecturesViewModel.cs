using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using Bitad2021.Data;
using Bitad2021.Models;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using Xamarin.Forms;

namespace Bitad2021.ViewModels
{
    public class LecturesViewModel
    {
        public IBitadService _bitadService;
        
        [Reactive]
        public ObservableCollection<Agenda> Lectures{ get; set; }
        
        public ReactiveCommand<Unit, Unit> LoadDataCommand;
        
        public LecturesViewModel(IBitadService bitadService = null)
        {
            
            _bitadService = bitadService ?? Locator.Current.GetService<IBitadService>();
            
            Lectures = new ObservableCollection<Agenda>();
            LoadDataCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                Lectures.AddRange(await _bitadService.GetAllAgendas());
            });

            LoadDataCommand.Execute();
        }
    }
}