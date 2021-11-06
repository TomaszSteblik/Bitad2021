using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Reactive.Disposables;
using Bitad2021.Models;
using Bitad2021.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms.Xaml;

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

        private void ViewModelOnOnQrResponseReceived(object sender, (QrCodeResponse, HttpStatusCode) response)
        {
            //AnimationView.Animation = response.Item1 is not null ? "star.json" : "fail_cross.json";
            //ViewModel.IsAnimationVisible = true;

            //AnimationView.PlayAnimation();

            if (response.Item1 is null)
                //show error message

                switch (response.Item2)
                {
                    case HttpStatusCode.Unauthorized:
                        SnackBarAnchor.DisplayToastAsync("Błąd autoryzacji");
                        break;
                    case HttpStatusCode.NoContent:
                        SnackBarAnchor.DisplayToastAsync("Błędny kod");
                        break;
                    default:
                        SnackBarAnchor.DisplayToastAsync("Nieznany błąd");
                        break;
                }
            else
                //show succes message;
                SnackBarAnchor.DisplayToastAsync($"Zdobyłeś {response.Item1.Points} punktów!");
        }


        private void AnimationView_OnOnFinishedAnimation(object sender, EventArgs e)
        {
            ViewModel.IsAnimationVisible = false;
        }
    }
}