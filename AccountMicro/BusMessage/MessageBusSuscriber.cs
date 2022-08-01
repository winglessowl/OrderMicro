using AccountMicro.BusMessage;
using AccountMicro.Events;
using AccountMicro.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json.Serialization;

namespace OrderMicro.BusMessage
{
    public class MessageBusSuscriber : BackgroundService
    {
        private  IConnection _connection;
        private  IModel _channel;
        private readonly IServiceProvider _service;
        public IMessageBusClient _publisher;
        public MessageBusSuscriber(IServiceProvider service,IMessageBusClient publisher)
        {
            _service = service;
            _publisher = publisher;
            InnitRabitMQ();
        }

        private void InnitRabitMQ()
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            try
            {

                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare("EG.OrderPlacedExchange", type: ExchangeType.Fanout);
                _channel.QueueDeclare("EG.OrderPlacedQueue", false, false, false, null);
                _channel.QueueBind(queue: "EG.OrderPlacedQueue",
                              exchange: "EG.OrderPlacedExchange",
                              routingKey: "EG.key");
              
            }
            catch (Exception ex)
            {
            }
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                EventProcessing(content);
            };
            _channel.BasicConsume("EG.OrderPlacedQueue", true, consumer);
            return Task.CompletedTask;
        }

        private void EventProcessing(string RawEvent) 
        {
            try
            {
                var orderPlacedEvent = JsonConvert.DeserializeObject<OrderPlacedEvent>(RawEvent);
                bool aproved = false;
                using (var scope = _service.CreateScope())
                {
                    var accountContext = scope.ServiceProvider.GetRequiredService<AccountMicro.Models.AccountContext>();
                    var OrderAcceptServices = new OrderAcceptService(accountContext);
                  aproved =  OrderAcceptServices.RecieveOrder(orderPlacedEvent);
                }
                
                   
                    _publisher.PublishEvent(new OrderAceptedEvent()
                    {
                        ClientId = orderPlacedEvent.IdClient,
                        IdOrder = orderPlacedEvent.OrderId,
                        Status = aproved ? "Open" : "Rejected"
                    });
                
                
              

            }
            catch (Exception ex) 
            {

            }

            }



    }
}
