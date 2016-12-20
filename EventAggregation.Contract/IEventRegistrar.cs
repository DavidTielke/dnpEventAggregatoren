namespace EventAggregation.Contract
{
    public interface IEventRegistrar
    {
        void Register<TSubscriber, TMessage>()
            where TSubscriber : IEventHandler<TMessage>
            where TMessage : EventMessage;
    }
}