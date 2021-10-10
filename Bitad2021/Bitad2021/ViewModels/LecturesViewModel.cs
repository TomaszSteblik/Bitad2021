using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using Bitad2021.Data;
using Bitad2021.Models;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Bitad2021.ViewModels
{
    public class LecturesViewModel : ReactiveObject
    {
        public IBitadService _bitadService;
        private IScreen _hostScreen;
        
        
        public ReactiveCommand<Unit, Unit> LoadDataCommand;
        public ReactiveCommand<Unit, IRoutableViewModel> ViewLectureCommand;
        public ReactiveCommand<Unit,Unit> LogoutCommand { get; set; }

        [Reactive]
        public ObservableCollection<Agenda> Lectures{ get; set; }
        [Reactive]
        public Agenda SelectedItem { get; set; }
        [Reactive] 
        public ListViewSelectionMode SelectionMode { get; set; } 
        
        
        public LecturesViewModel(IBitadService bitadService = null, IScreen hostScreen = null)
        {
            _hostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            _bitadService = bitadService ?? Locator.Current.GetService<IBitadService>();
            Lectures = new ObservableCollection<Agenda>();

            LoadDataCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    ReactiveCommand.CreateFromObservable(() =>
                        hostScreen.Router.NavigateAndReset.Execute(new LoginViewModel())).Execute();
                    return;
                }
                
                Lectures.AddRange(await _bitadService.GetAllAgendas());
            });

            SelectedItem = new Agenda();
            

            ViewLectureCommand = ReactiveCommand.CreateFromObservable(() =>
            {

                
                var agenda = new Agenda()
                {
                    End = DateTime.FromOADate(SelectedItem.End.ToOADate()),
                    Room = SelectedItem.Room,
                    Speaker = new Speaker()
                    {
                        Company = SelectedItem.Speaker.Company,
                        Description = SelectedItem.Speaker.Description,
                        Name = SelectedItem.Speaker.Name,
                        Picture = SelectedItem.Speaker.Picture,
                        Website = SelectedItem.Speaker.Website,
                        WebsiteLink = SelectedItem.Speaker.WebsiteLink
                    },
                    Start = DateTime.FromOADate(SelectedItem.Start.ToOADate()),
                    Title = SelectedItem.Title,
                    Description = SelectedItem.Description
                };
                
                SelectionMode = ListViewSelectionMode.None;
                SelectionMode = ListViewSelectionMode.Single;
                
                if (agenda.Description is null)
                    return null;

                return  _hostScreen.Router.Navigate.Execute(new LectureViewModel(agenda));
            });

            ViewLectureCommand.ThrownExceptions.Subscribe(ex => Debug.WriteLine(ex.Message));
      
            LoadDataCommand.Execute();
        }
    }
}