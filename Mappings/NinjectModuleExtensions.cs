using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bootstrapper.Contract;
using Ninject.Modules;
using Ninject.Planning.Bindings;

namespace Mappings
{
    internal static class NinjectModuleExtensions
    {
        public static void RegisterActivator<TActivator>(this NinjectModule source)
            where TActivator : IActivateable
        {
            source.Bind<IActivateable>().To<TActivator>();
        }

        public static void RegisterDeactivator<TDeactivator>(this NinjectModule source)
            where TDeactivator : IDeactivateable
        {
            source.Bind<IDeactivateable>().To<TDeactivator>();
        } 
    }
}
