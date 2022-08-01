using AccountMicro.Events;

namespace AccountMicro.BusMessage
{
    public interface IMessageBusClient
    {
        void PublishEvent(IntegrationEvent Event);
    }
}
