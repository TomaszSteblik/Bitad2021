using System.Reflection;
using Bitad2021.Data.Implementations;
using Bitad2021.Data.Interfaces;
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
            Locator.CurrentMutable.RegisterConstant<IAgendaService>(new AgendaServiceRest());
            Locator.CurrentMutable.RegisterConstant<IUserService>(new UserServiceRest());
            Locator.CurrentMutable.RegisterConstant<IWorkshopService>(new WorkshopServiceRest());
        }

        private void RegisterViewModels()
        {
            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetAssembly(GetType()));
        }
    }
}