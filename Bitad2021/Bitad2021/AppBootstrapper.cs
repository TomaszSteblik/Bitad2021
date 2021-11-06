using System.Reflection;
using Bitad2021.Data;
using Bitad2021.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Bitad2021
{
    public class AppBootstrapper : IScreen
    {
        public AppBootstrapper()
        {
            Router = new RoutingState();

            Locator.CurrentMutable.RegisterConstant<IScreen>(this);
            var bitadService = new BitadServiceRest();
            Locator.CurrentMutable.RegisterConstant<IBitadService>(bitadService);

            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetExecutingAssembly());
            //TODO: Splash screen for ios
            var hasPassword = Preferences.ContainsKey("password");
            var hasUsername = Preferences.ContainsKey("username");
            if (hasPassword && hasUsername)
            {
                var password = Preferences.Get("password", "");
                var username = Preferences.Get("username", "");

                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    Router.Navigate.Execute(new LoginViewModel());
                    return;
                }

                var res = bitadService.LoginSync(username, password);
                if (res is null)
                    Router.Navigate.Execute(new LoginViewModel());
                else
                    Router.NavigateAndReset.Execute(new TabbedViewModel(res));
            }
            else
            {
                Router.NavigateAndReset.Execute(new LoginViewModel());
            }
        }

        public RoutingState Router { get; }

        public Page CreateMainPage()
        {
            return new RoutedViewHost();
        }
    }
}