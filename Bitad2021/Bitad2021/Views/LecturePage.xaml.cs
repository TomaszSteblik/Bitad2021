using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bitad2021.ViewModels;
using ReactiveUI.XamForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bitad2021.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LecturePage : ReactiveContentPage<LectureViewModel>
    {
        public LecturePage()
        {
            InitializeComponent();
        }
    }
}