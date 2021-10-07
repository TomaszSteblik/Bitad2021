using System.Reactive;
using Bitad2021.Models;
using ReactiveUI;
using Splat;
using Xamarin.Essentials;

namespace Bitad2021.ViewModels
{
    public class WorkshopViewModel : ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment => "";
        public IScreen HostScreen { get; }
        public Workshop Workshop { get; set; }
        public ReactiveCommand<Unit, IRoutableViewModel> ViewSpeakerCommand { get; set; }
        public ReactiveCommand<string,Unit> TapLinkCommand { get; set; }

        public WorkshopViewModel(Workshop workshop, IScreen hostScreen = null)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            Workshop = workshop;
            
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
        }
    }
}