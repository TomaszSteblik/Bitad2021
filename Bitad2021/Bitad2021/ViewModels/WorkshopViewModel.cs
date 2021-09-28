using Bitad2021.Models;
using ReactiveUI;

namespace Bitad2021.ViewModels
{
    public class WorkshopViewModel : ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment => Workshop.Title;
        public IScreen HostScreen { get; }
        public Workshop Workshop { get; set; }

        public WorkshopViewModel(Workshop workshop)
        {
            Workshop = workshop;
        }
    }
}