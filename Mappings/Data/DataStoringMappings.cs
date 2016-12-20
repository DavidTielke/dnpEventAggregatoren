using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataClasses;
using DataStoring.Contract;
using DataStoring.CSV;
using Ninject.Modules;

namespace Mappings.Data
{
    class DataStoringMappings : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository<Person>>().To<PersonRepository>();
        }
    }
}
