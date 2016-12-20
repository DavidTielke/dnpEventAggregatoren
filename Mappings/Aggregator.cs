using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mappings.CrossCutting;
using Mappings.Data;
using Mappings.Logic;
using Ninject.Modules;
using PersonManagement;

namespace Mappings
{
    public class Aggregator
    {
        public INinjectModule[] Mappings => new INinjectModule[]
        {
            new DataStoringMappings(), 
            new PersonMangementMappings(), 
            new ConfigurationMappings(), 
            new EventAggregatorMappings(), 
            new BootstrapperMappings(), 
        };
    }
}
