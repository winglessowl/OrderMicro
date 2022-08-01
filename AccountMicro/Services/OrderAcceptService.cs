using AccountMicro.Events;
using AccountMicro.Models;

namespace AccountMicro.Services
{
    public class OrderAcceptService : IOrderService
    {
        private AccountContext _context;
        public OrderAcceptService(AccountContext context) 
        {
            _context = context;
        }
        public bool RecieveOrder(IntegrationEvent _event)
        {
            bool acepted = false;
            var orderPlacedEvent = (OrderPlacedEvent)_event;
            var client = _context.Accounts.Where(x => x.Id == orderPlacedEvent.IdClient).FirstOrDefault();
            if(client != null && client.CheckFunds(orderPlacedEvent.Amount * orderPlacedEvent.Price)) 
            {
                client.ReserveFunds(orderPlacedEvent.Amount * orderPlacedEvent.Price);
                _context.SaveChanges();
                acepted = true;
            }
            return acepted;
        }
    }
}
