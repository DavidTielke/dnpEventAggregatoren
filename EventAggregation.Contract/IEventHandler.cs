namespace EventAggregation.Contract
{
    public interface IEventHandler<TMessage>
        where TMessage : EventMessage
    {
        void HandleMessage(object sender, TMessage message);
    }
}