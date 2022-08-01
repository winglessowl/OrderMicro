using Newtonsoft.Json;
using OrderMicro.Events;
using OrderMicro.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace OrderMicro.BusMessage
{
    public class MessageBusSuscriber : BackgroundService
    {
        private  IConnection _connection;
        private  IModel _channel;
        private readonly IServiceProvider _service;
        public MessageBusSuscriber(IServiceProvider service) 
        {
            _service = service;
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

                _channel.ExchangeDeclare("EG.OrderAcceptedExchange", type: ExchangeType.Fanout);
                _channel.QueueDeclare("EG.OrderAcceptedQueue", false, false, false, null);
                _channel.QueueBind("EG.OrderAcceptedQueue", "EG.OrderAcceptedExchange", "EG.key");
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
            _channel.BasicConsume("EG.OrderAcceptedQueue", true, consumer);
            return Task.CompletedTask;
        }

        private void EventProcessing(string RawEvent) 
        {
            try
            {
                var orderPlacedEvent = JsonConvert.DeserializeObject<OrderAceptedEvent>(RawEvent);
                bool aproved = false;
                using (var scope = _service.CreateScope())
                {
                    var accountContext = scope.ServiceProvider.GetRequiredService<OrderMicro.Model.OrderContext>();
                    var OrderAcceptServices = new OrderAceptedService(accountContext);
                    aproved = OrderAcceptServices.RecieveOrder(orderPlacedEvent);
                }
            }
            catch (Exception ex)
            {

            }
        }



    }
}
