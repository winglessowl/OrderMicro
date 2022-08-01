using AccountMicro.Events;

namespace AccountMicro.Services
{
    public interface IOrderService
    {
        bool RecieveOrder(IntegrationEvent _event);
    }
}
