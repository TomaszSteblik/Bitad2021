using System.Reactive;
using Bitad2021.Models;
using ReactiveUI;
using Splat;
using Xamarin.Essentials;

namespace Bitad2021.ViewModels
{
    public class SpeakerViewModel : ReactiveObject, IRoutableViewModel
    {
        public SpeakerViewModel(Speaker speaker, IScreen screen = null)
        {
            Speaker = speaker;
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();

            TapLinkCommand = ReactiveCommand.CreateFromTask(async (string url) => { await Launcher.OpenAsync(url); });
        }

        public Speaker Speaker { get; set; }
        public ReactiveCommand<string, Unit> TapLinkCommand { get; set; }
        public string? UrlPathSegment => "";
        public IScreen HostScreen { get; }
    }
}