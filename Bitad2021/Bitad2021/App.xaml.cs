using System;
using Bitad2021.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Bitad2021
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var bootstrapper = new AppBootstrapper();
            MainPage = bootstrapper.CreateMainPage();
            //MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}