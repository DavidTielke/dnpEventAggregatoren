using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventAggregation.Contract;

namespace DataStoring.Contract.Messages
{
    public class EntityModifiedMessage : EventMessage
    {
        public string Status { get; set; }
        public object Entity { get; set; }
    }
}
