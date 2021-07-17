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
        
        public QrScannerViewModel(IBitadService bitadService = null)
        {
            IsScanning = true;
            IsAnalyzing = true;
            
            _bitadService = bitadService ?? Locator.Current.GetService<IBitadService>();

            
            ScanResultCommand = new Command(async () =>
            {
                Debug.WriteLine(IsAnalyzing);

                IsAnalyzing = false;
                
                var response = await _bitadService.RedeemQrCode(Result.Text);

                if (response is null)
                {
                    //show error message
                    Debug.WriteLine("ERROR");
                }
                else
                {
                    //show succes message;
                    Debug.WriteLine($"{response.QrCode} for {response.Points} points");
                }

                IsAnalyzing = true;
            });

        }
    }
}