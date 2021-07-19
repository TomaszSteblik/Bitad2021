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
        public WorkshopsViewModel WorkshopsViewModel { get; set; }
        
        [Reactive]
        public LecturesViewModel LecturesViewModel { get; set; }
        
        public AgendasViewModel(IBitadService bitadService = null)
        {
            _bitadService = bitadService ?? Locator.Current.GetService<IBitadService>();

            LecturesViewModel = new LecturesViewModel();

            WorkshopsViewModel = new WorkshopsViewModel();

        }
    }
}