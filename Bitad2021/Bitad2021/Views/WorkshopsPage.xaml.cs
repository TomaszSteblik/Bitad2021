using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bitad2021.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bitad2021.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkshopsPage : ReactiveContentPage<WorkshopsViewModel>
    {
        public WorkshopsPage()
        {
            InitializeComponent();
            this.WhenActivated(d =>
            {
                d(this.BindCommand(
                    this.ViewModel,
                    vm => vm.ViewWorkshopCommand,
                    v => v.ListView,
                    nameof(ListView.ItemSelected)));
                ListView.SelectionMode = ListViewSelectionMode.Single;
            });
        }
    }
}