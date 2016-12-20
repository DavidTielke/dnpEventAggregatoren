using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventAggregation.Contract;

namespace Configuration.Contracts.Messages
{
    public class ConfigurationChangedMessage : EventMessage
    {
        public string Category { get; set; }
        public string Key { get; set; }
    }
}
