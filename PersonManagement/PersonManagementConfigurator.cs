using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Configuration.Contracts;
using Configuration.Contracts.Messages;
using EventAggregation.Contract;

namespace PersonManagement
{
    // Todo: Passiver Eventhandler
    public class PersonManagementConfigurator : IEventHandler<ConfigurationChangedMessage>
    {
        private readonly IConfigurator _config;
        public int AgeTreshold { get; set; }

        public PersonManagementConfigurator(IConfigurator config)
        {
            _config = config;
            AgeTreshold = _config.Get("PersonManagement", "AgeTreshold", 18);
        }

        public void HandleMessage(object sender, ConfigurationChangedMessage message)
        {
            if (message.Category == "PersonManagement" && message.Key == "AgeTreshold")
            {
                AgeTreshold = _config.Get("PersonManagement", "AgeTreshold", 18);
            }
        }
    }
}
