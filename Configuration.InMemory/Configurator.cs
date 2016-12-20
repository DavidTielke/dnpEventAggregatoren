using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Configuration.Contracts;
using Configuration.Contracts.Exceptions;
using Configuration.Contracts.Messages;
using EventAggregation.Contract;

namespace Configuration.InMemory
{
    public class Configurator : IConfigurator
    {
        private readonly IEventAggregator _eventAggregator;
        private Dictionary<string, object> _items;

        public Configurator(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _items = new Dictionary<string, object>();
        }

        private string CreateKey(string category, string key) => $"{category}.{key}";

        public TType Get<TType>(string category, string key, TType defaultValue = default(TType))
        {
            var itemKey = CreateKey(category, key);
            var containsKey = _items.ContainsKey(itemKey);
            if (!containsKey)
            {
                // Todo: Zwei Überladungen für default und ohne
                return defaultValue;
            }

            var value = (TType) _items[itemKey];
            return value;
        }

        public void Set<TType>(string category, string key, TType value)
        {
            var itemKey = CreateKey(category, key);
            _items[itemKey] = value;

            var message = new ConfigurationChangedMessage
            {
                Category = category,
                Key = key
            };
            _eventAggregator.Raise(this, message);
        }
    }
}
