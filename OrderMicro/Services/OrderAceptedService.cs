using OrderMicro.Events;
using OrderMicro.Model;

namespace OrderMicro.Services
{
    public class OrderAceptedService
    {
        private OrderContext _context;
        public OrderAceptedService(OrderContext context)
        {
            _context = context;
        }
        public bool RecieveOrder(IntegrationEvent _event)
        {
            bool acepted = false;
            var orderAceptedEvent = (OrderAceptedEvent)_event;
            var order = _context.Orders.Where(x => x.IdClient == orderAceptedEvent.ClientId && x.Id == orderAceptedEvent.IdOrder).FirstOrDefault();
            if (order != null )
            {
                order.Status = orderAceptedEvent.Status;
                _context.SaveChanges();
                acepted = true;
            }
            return acepted;
        }
    }
}
