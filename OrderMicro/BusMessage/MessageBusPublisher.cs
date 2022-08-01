using Newtonsoft.Json;
using OrderMicro.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace OrderMicro.BusMessage
{
    public class MessageBusPublisher : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusPublisher(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "EG.OrderPlacedExchange", type: ExchangeType.Fanout);


            }
            catch (Exception ex)
            {
            }
        }


        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "EG.OrderPlacedExchange",
                            routingKey: "EG.key",
                            basicProperties: null,
                            body: body);
            Console.WriteLine($"--> We have sent {message}");
        }

        public void Dispose()
        {
            Console.WriteLine("MessageBus Disposed");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> RabbitMQ Connection Shutdown");
        }

      

        public void PublishEvent(IntegrationEvent Event)
        {
            var message = JsonConvert.SerializeObject(Event);

            if (_connection.IsOpen)
            {
                SendMessage(message);
            }
        }
    }
}
