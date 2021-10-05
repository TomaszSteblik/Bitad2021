using System.Diagnostics;
using System.Reactive;
using Bitad2021.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using Xamarin.Essentials;

namespace Bitad2021.ViewModels
{
    public class LectureViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment => "Lecture.Title";
        public IScreen HostScreen { get; }

        [Reactive]
        public Agenda Lecture { get; set; }

        public ReactiveCommand<string,Unit> TapLinkCommand { get; set; }
        public ReactiveCommand<Unit, IRoutableViewModel> ViewSpeakerCommand { get; set; }

        public LectureViewModel(Agenda agenda, IScreen hostScreen = null)
        {
            Lecture = agenda;
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();

            TapLinkCommand = ReactiveCommand.CreateFromTask(async (string url) =>
            {
                await Launcher.OpenAsync(url);
            });

            ViewSpeakerCommand = ReactiveCommand.CreateFromObservable(() =>
            {
                var speaker = new Speaker()
                {
                    Company = Lecture.Speaker.Company,
                    Description = Lecture.Speaker.Description,
                    Name = Lecture.Speaker.Name,
                    Picture = Lecture.Speaker.Picture,
                    Website = Lecture.Speaker.Website,
                    WebsiteLink = Lecture.Speaker.WebsiteLink
                };
                return HostScreen.Router.Navigate.Execute(new SpeakerViewModel(speaker));
            });
            
        }
    }
}