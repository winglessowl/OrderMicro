using OrderMicro.Events;

namespace OrderMicro.BusMessage
{
    public interface IMessageBusClient
    {
        void PublishEvent(IntegrationEvent Event);
    }
}
