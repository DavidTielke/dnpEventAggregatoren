using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Configuration.Contracts;
using Configuration.InMemory;
using Ninject.Modules;

namespace Mappings.CrossCutting
{
    class ConfigurationMappings : NinjectModule
    {
        public override void Load()
        {
            Bind<IConfigurator>().To<Configurator>();
        }
    }
}
