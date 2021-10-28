using System.Reactive;
using System.Threading.Tasks;
using Bitad2021.Data;
using Bitad2021.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using Xamarin.Essentials;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace Bitad2021.ViewModels
{
    public class WorkshopViewModel : ReactiveObject, IRoutableViewModel
    {
        private static bool _selectedWorkshop = false;
        public string? UrlPathSegment => "";
        public IScreen HostScreen { get; }
        private IBitadService _bitadService;
        private bool _userHasWorkshop;
        public Workshop Workshop { get; set; }
        public ReactiveCommand<Unit, IRoutableViewModel> ViewSpeakerCommand { get; set; }
        public ReactiveCommand<string,Unit> TapLinkCommand { get; set; }
        public ReactiveCommand<Unit,Unit> SelectWorkshopCommand { get; set; }
        //TODO: CANT USE THIS IF USER HAVE A WORKSHOP ALREADY
        [Reactive]
        public bool IsSelectWorkshopButtonVisible { get; set; }

        public bool IsShortInfoVisible => Workshop.ShortInfo is not null;
        public bool IsExternalLinkVisible => Workshop.ExternalLink is not null;

        public WorkshopViewModel(bool userHasWorkshop,Workshop workshop, IScreen hostScreen = null, IBitadService bitadService = null)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            Workshop = workshop;
            _bitadService = bitadService ?? Locator.Current.GetService<IBitadService>();
            _userHasWorkshop = userHasWorkshop;
            
            IsSelectWorkshopButtonVisible = (Workshop.MaxParticipants > this.Workshop.ParticipantsNumber) 
                                            && !_userHasWorkshop && !_selectedWorkshop;

            TapLinkCommand = ReactiveCommand.CreateFromTask(async (string url) =>
            {
                await Launcher.OpenAsync(url);
            });

            ViewSpeakerCommand = ReactiveCommand.CreateFromObservable(() =>
            {
                var speaker = new Speaker()
                {
                    Company = Workshop.Speaker.Company,
                    Description = Workshop.Speaker.Description,
                    Name = Workshop.Speaker.Name,
                    Picture = Workshop.Speaker.Picture,
                    Website = Workshop.Speaker.Website,
                    WebsiteLink = Workshop.Speaker.WebsiteLink
                };
                return HostScreen.Router.Navigate.Execute(new SpeakerViewModel(speaker));
            });
            
            SelectWorkshopCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Application.Current.MainPage.DisplayToastAsync("Błąd połączenia");
                    await Task.Delay(10000);
                    return;
                }
                
                var response = await _bitadService.SelectWorkshop(Workshop.Code);

                if (response)
                {
                    await Application.Current.MainPage.DisplayToastAsync("Zapisałeś się na warsztat!");
                    _selectedWorkshop = true;
                    IsSelectWorkshopButtonVisible = false;
                }
                else
                {
                    await Application.Current.MainPage.DisplayToastAsync("Nieznany błąd");
                }
                
            });
        }
    }
}