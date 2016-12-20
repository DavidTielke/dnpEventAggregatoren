namespace EventAggregation.Contract
{
    public interface IEventAggregator
    {
        void Raise<TMessage>(object sender, TMessage message)
            where TMessage : EventMessage;
    }
}