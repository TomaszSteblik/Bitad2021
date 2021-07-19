using System.Collections.Generic;
using System.Collections.ObjectModel;
using Bitad2021.Models;
using ReactiveUI.Fody.Helpers;

namespace Bitad2021.ViewModels
{
    public class WorkshopsViewModel
    {
        [Reactive]
        public ObservableCollection<Workshop> Workshops { get; set; }
        public WorkshopsViewModel()
        {
            
        }
    }
}