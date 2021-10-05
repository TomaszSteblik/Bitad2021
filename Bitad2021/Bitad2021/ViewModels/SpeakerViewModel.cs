using Bitad2021.Models;
using ReactiveUI;
using Splat;

namespace Bitad2021.ViewModels
{
    public class SpeakerViewModel : ReactiveObject,IRoutableViewModel
    {
        public string? UrlPathSegment => "";
        public IScreen HostScreen { get; }
        public Speaker Speaker { get; set; }
        public SpeakerViewModel(Speaker speaker,IScreen screen = null)
        {
            Speaker = speaker;
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
        }

        
    }
}