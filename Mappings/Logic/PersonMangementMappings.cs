using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using PersonManagement;
using PersonManagement.Contract;
using Mappings;

namespace Mappings.Logic
{
    class PersonMangementMappings : NinjectModule
    {
        public override void Load()
        {
            this.RegisterActivator<PersonManagementLifecycle>();
            this.RegisterDeactivator<PersonManagementLifecycle>();
            Bind<IPersonManager>().To<PersonManager>();
        }
    }
}
