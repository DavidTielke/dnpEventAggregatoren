using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventAggregation;
using EventAggregation.Contract;
using Ninject.Modules;

namespace Mappings.CrossCutting
{
    class EventAggregatorMappings : NinjectModule
    {
        public override void Load()
        {
            var aggregator = new EventAggregator(this.Kernel);
            Bind<IEventAggregator>().ToConstant(aggregator);
            Bind<IEventMapper>().ToConstant(aggregator);
            Bind<IEventRegistrar>().ToConstant(aggregator);
        }
    }
}
