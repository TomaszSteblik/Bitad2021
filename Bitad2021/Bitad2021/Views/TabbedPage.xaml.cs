using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Bitad2021.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace Bitad2021.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedView : ReactiveTabbedPage<TabbedViewModel>
    {
        public TabbedView()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.WhenAnyValue(x => x.CurrentPage).InvokeCommand(new Command(() =>
                {
                    if (CurrentPage is QrScannerPage qrScannerPage)
                    {
                        qrScannerPage.ViewModel.IsAnalyzing = true;
                    }
                    else if (GetPageByIndex(1) is QrScannerPage qrScanner)
                    {
                        qrScanner.ViewModel.IsAnalyzing = false;
                    }
                })).DisposeWith(disposable);
            });
        }
    }
}