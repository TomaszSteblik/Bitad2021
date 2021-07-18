using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Bitad2021.ViewModels;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace Bitad2021.Views
{
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QrScannerPage : ReactiveContentPage<QrScannerViewModel>
    {
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public QrScannerPage()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                ViewModel.IsAnalyzing = true;
                Debug.WriteLine("qr activated");
                
                ViewModel.OnQrResponseReceived += ViewModelOnOnQrResponseReceived;
                
                Disposable
                    .Create(() =>
                    {
                        ViewModel.IsAnalyzing = false;
                        ViewModel.OnQrResponseReceived -= ViewModelOnOnQrResponseReceived;
                        Debug.WriteLine("qr deactivated");
                    }).DisposeWith(disposable);
                
            });
        }

        private void ViewModelOnOnQrResponseReceived(object sender, bool success)
        {
            AnimationView.Animation = success ? "star.json" : "fail_cross.json";
            ViewModel.IsAnimationVisible = true;
            AnimationView.PlayAnimation();

        }


        private void AnimationView_OnOnFinishedAnimation(object sender, EventArgs e)
        {
            ViewModel.IsAnimationVisible = false;
        }
    }
}