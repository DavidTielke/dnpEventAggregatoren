using System;

namespace EventAggregation.Contract
{
    public interface IEventMapper
    {
        void Map<TSourceMessage>(Action<IEventAggregator, TSourceMessage> projection);
    }
}