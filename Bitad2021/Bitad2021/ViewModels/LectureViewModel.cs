using Bitad2021.Models;
using ReactiveUI;

namespace Bitad2021.ViewModels
{
    public class LectureViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment => Lecture.Title;
        public IScreen HostScreen { get; }

        public Agenda Lecture { get; set; }

        public LectureViewModel(Agenda agenda)
        {
            Lecture = agenda;
        }
    }
}