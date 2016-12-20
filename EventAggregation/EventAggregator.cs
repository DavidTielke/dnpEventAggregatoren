using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventAggregation.Contract;
using Ninject;

namespace EventAggregation
{
    public class EventAggregator : IEventRegistrar, IEventAggregator, IEventMapper
    {
        private readonly IKernel _kernel;
        private Dictionary<Type, List<Type>> _registrations;
        private Dictionary<Type, List<Delegate>> _mappings;

        public EventAggregator(IKernel kernel)
        {
            _kernel = kernel;
            // WICHTIG: Hierbei handelt es sich um ein Beispiel
            // In einer richtigen Implementierung würde hier
            // mit WearkReference<T> gearbeitet, um MemoryLeaks
            // zu verhindern
            _registrations = new Dictionary<Type, List<Type>>();
            _mappings = new Dictionary<Type, List<Delegate>>();
        }

        private bool SubscribersExist<TMessage>()
            where TMessage : EventMessage
        {
            var messageType = typeof(TMessage);
            var exist = _registrations.ContainsKey(messageType) && _registrations[messageType].Count > 0;
            return exist;
        }

        private bool MappingExist<TMessage>()
        {
            var messageType = typeof(TMessage);
            var exist = _mappings.ContainsKey(messageType) && _mappings[messageType].Count > 0;
            return exist;
        }

        public void Register<TSubscriber, TMessage>()
            where TSubscriber : IEventHandler<TMessage>
            where TMessage : EventMessage
        {
            var alreadyRegistrationsExist = SubscribersExist<TMessage>();
            if (!alreadyRegistrationsExist)
            {
                _registrations[typeof(TMessage)] = new List<Type>();
            }

            _registrations[typeof(TMessage)].Add(typeof(TSubscriber));
        }

        public void Raise<TMessage>(object sender, TMessage message)
            where TMessage : EventMessage
        {
            var messageType = typeof(TMessage);
            var exceptions = new List<Exception>();

            var registrationsExist = SubscribersExist<TMessage>();
            if (registrationsExist)
            {
                var typesToNotify = _registrations[messageType];
                foreach (var typeToNotify in typesToNotify)
                {
                    try
                    {
                        var instance = (IEventHandler<TMessage>)_kernel.Get(typeToNotify);
                        instance.HandleMessage(sender, message);
                    }
                    catch (Exception e)
                    {
                        exceptions.Add(e);
                    }
                }
            }

            var mappingsExist = MappingExist<TMessage>();
            if (mappingsExist)
            {
                var mappings = _mappings[messageType];
                foreach (var mapping in mappings)
                {
                    mapping.DynamicInvoke(this, message);
                }
            }

            if (exceptions.Any())
            {
                throw new AggregateException("Error publishing message", exceptions);
            }
        }

        public void Map<TSourceMessage>(Action<IEventAggregator, TSourceMessage> projection)
        {
            var typeMessage = typeof(TSourceMessage);
            var alreadyMappingsExist = MappingExist<TSourceMessage>();
            if (!alreadyMappingsExist)
            {
                _mappings[typeMessage] = new List<Delegate>();
            }
            _mappings[typeMessage].Add(projection);
        }
    }
}
