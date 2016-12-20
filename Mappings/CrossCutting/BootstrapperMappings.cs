using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bootstrapper.Contract;
using Ninject.Modules;

namespace Mappings.CrossCutting
{
    class BootstrapperMappings : NinjectModule
    {
        public override void Load()
        {
            Bind<IBootstrapper>().To<Bootstrapper.Bootstrapper>().InSingletonScope();
        }
    }
}
