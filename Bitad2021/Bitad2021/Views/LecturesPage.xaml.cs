using Bitad2021.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bitad2021.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LecturesPage : ReactiveContentPage<LecturesViewModel>
    {
        public LecturesPage()
        {
            InitializeComponent();
            this.WhenActivated(
                d =>
                {
                    d(this.BindCommand(
                        this.ViewModel,
                        vm => vm.ViewLectureCommand,
                        v => v.ListView,
                        nameof(ListView.ItemSelected)));
                    ListView.SelectionMode = ListViewSelectionMode.Single;
                });
        }
    }
}