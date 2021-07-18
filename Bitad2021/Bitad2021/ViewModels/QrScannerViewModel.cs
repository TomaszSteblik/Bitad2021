using System;
using System.Diagnostics;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using Bitad2021.Data;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using Xamarin.Forms;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace Bitad2021.ViewModels
{
    public class QrScannerViewModel : ReactiveObject
    {
        private IBitadService _bitadService;
        public ICommand ScanResultCommand { get; set; }
        [Reactive]
        public Result Result { get; set; }
        [Reactive] 
        public bool IsAnalyzing { get; set; }    
        [Reactive] 
        public bool IsScanning { get; set; }
        [Reactive] 
        public bool IsAnimationVisible { get; set; }

        public event EventHandler<bool> OnQrResponseReceived;
        
        
        public QrScannerViewModel(IBitadService bitadService = null)
        {
            IsScanning = true;
            IsAnalyzing = true;
            IsAnimationVisible = true;
            
            _bitadService = bitadService ?? Locator.Current.GetService<IBitadService>();

            
            ScanResultCommand = new Command(async () =>
            {

                IsAnalyzing = false;

                var response = await _bitadService.RedeemQrCode(Result.Text);
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (response is null)
                    {
                        //show error message
                        OnQrResponseReceived?.Invoke(this, false);

                        Debug.WriteLine("ERROR");
                    }
                    else
                    {
                        //show succes message;
                        OnQrResponseReceived?.Invoke(this, true);

                        Debug.WriteLine($"{response.QrCode} for {response.Points} points");
                    }
                });

                await Task.Delay(5000);
                IsAnalyzing = true;
            });

        }
    }
}