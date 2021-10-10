using System;
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
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Bitad2021.ViewModels
{
    public class WorkshopsViewModel : ReactiveObject
    {
        public IBitadService _bitadService;
        private IScreen _hostScreen;
        
        [Reactive]
        public ObservableCollection<Workshop> Workshops { get; set; }
        public ReactiveCommand<Unit,Unit> LoadDataCommand { get; set; }
        public ReactiveCommand<Unit,IRoutableViewModel> ViewWorkshopCommand { get; set; }
        public Workshop SelectedItem { get; set; }
        [Reactive]
        public ListViewSelectionMode SelectionMode { get; set; }

        public WorkshopsViewModel(IBitadService bitadService = null, IScreen hostScreen = null)
        {
            _hostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            _bitadService = bitadService ?? Locator.Current.GetService<IBitadService>();

            Workshops = new ObservableCollection<Workshop>();

            LoadDataCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    ReactiveCommand.CreateFromObservable(() =>
                        hostScreen.Router.NavigateAndReset.Execute(new LoginViewModel())).Execute();
                    return;
                }
                
                Workshops.AddRange(await _bitadService.GetAllWorkshops());
            });
            LoadDataCommand.ThrownExceptions.Subscribe(ex => Debug.WriteLine(ex.Message));
            ViewWorkshopCommand = ReactiveCommand.CreateFromObservable(() =>
            {
                var workshop = new Workshop()
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
                    Description = SelectedItem.Description,
                    ParticipantsNumber = SelectedItem.ParticipantsNumber,
                    Code = SelectedItem.Code,
                    MaxParticipants = SelectedItem.MaxParticipants
                };
                ViewWorkshopCommand.ThrownExceptions.Subscribe(ex => Debug.WriteLine(ex.Message));
                //I know DRY, but got lazy
                // Selection mode is set back to single in LecturesPage.xaml.cs in OnActivated()
                // Setting it here to None automatically deselects all items if anything was selected
                SelectionMode = ListViewSelectionMode.None;
                return _hostScreen.Router.Navigate.Execute(new WorkshopViewModel(workshop));;
            });
            
            LoadDataCommand.Execute();
            
        }
    }
}