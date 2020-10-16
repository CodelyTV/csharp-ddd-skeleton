using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq
{
    public class RabbitMqConfig
    {
        public ConnectionFactory ConnectionFactory { get; }
        private static IConnection _connection { get; set; }
        private static IModel _channel { get; set; }

        public RabbitMqConfig(IOptions<RabbitMqConfigParams> rabbitMqParams)
        {
            var configParams = rabbitMqParams.Value;

            ConnectionFactory = new ConnectionFactory
            {
                HostName = configParams.HostName,
                UserName = configParams.Username,
                Password = configParams.Password,
                Port = configParams.Port
            };
        }

        public IConnection Connection()
        {
            if (_connection == null) _connection = ConnectionFactory.CreateConnection();
            return _connection;
        }

        public IModel Channel()
        {
            if (_channel == null) _channel = Connection().CreateModel();
            return _channel;
        }
    }
}
