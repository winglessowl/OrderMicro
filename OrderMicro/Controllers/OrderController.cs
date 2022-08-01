using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderMicro.BusMessage;
using OrderMicro.Events;
using OrderMicro.Model;

namespace OrderMicro.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        public IMessageBusClient _publisher;
        public OrderContext _context;
        public OrderController(IMessageBusClient publisher, OrderContext context)
        {
            _publisher = publisher;
            _context = context;

        }
        [HttpPost(Name = "CreateOrder")]
        public Order Get(int idClient,string symbol,int price,int amount)
        {
            Order orderCreated = new Order()
            {
                IdClient = idClient,
                Symbol = symbol,
                Price = price,
                Amount = amount,
                Status = "CREATED"
            };
            _context.Add(orderCreated);
            _context.SaveChanges();
            _publisher.PublishEvent(new OrderPlacedEvent { Amount = orderCreated.Amount, IdClient = orderCreated.IdClient, OrderId = orderCreated.Id, Price = orderCreated.Price });
            return orderCreated;
        }

        [HttpGet(Name = "GetOrders")]
        public List<Order> GetOrders(int clientID)
        {
            List<Order> orders = _context.Orders.Where(x => x.IdClient == clientID).ToList();
            return orders;
        }
    }
}
