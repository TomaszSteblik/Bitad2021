using System;
using System.Net.Http;
using System.Reflection;
using Bitad2021.Data;
using ReactiveUI;
using Splat;

namespace Bitad2021
{
    public class AppBootstrapper
    {
        public AppBootstrapper()
        {
            RegisterServices();
            RegisterViewModels();
            
        }

        private void RegisterServices()
        {
            Locator.CurrentMutable.RegisterConstant<IBitadService>(new BitadServiceRest());
            
        }

        private void RegisterViewModels()
        {
            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetExecutingAssembly());
        }
    }
}