using System;
using System.Net.Http;
using System.Reflection;
using Bitad2021.Data;
using Bitad2021.ViewModels;
using Bitad2021.Views;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
using Xamarin.Forms;

namespace Bitad2021
{
    public class AppBootstrapper : IScreen
    {
        public AppBootstrapper()
        {
            Router = new RoutingState();
            
            Locator.CurrentMutable.RegisterConstant<IScreen>(this);
            
            Locator.CurrentMutable.RegisterConstant<IBitadService>(new BitadServiceRest());
            
            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetExecutingAssembly());

            Router.Navigate.Execute(new LoginViewModel());


        }
        
        public Page CreateMainPage()
        {
            return new RoutedViewHost();
        }

        public RoutingState Router { get; }
    }
}