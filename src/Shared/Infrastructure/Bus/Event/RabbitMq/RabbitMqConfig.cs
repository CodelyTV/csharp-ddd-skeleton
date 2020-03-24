namespace CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq
{
    using Microsoft.Extensions.Options;
    using RabbitMQ.Client;

    public class RabbitMqConfig
    {
        public ConnectionFactory ConnectionFactory { get; private set; }
        private static IConnection _conection { get; set; }
        private static IModel _channel { get; set; }

        public RabbitMqConfig(IOptions<RabbitMqConfigParams> rabbitMqParams)
        {
            var configParams = rabbitMqParams.Value;

            ConnectionFactory = new ConnectionFactory()
            {
                HostName = configParams.HostName,
                UserName = configParams.Username,
                Password = configParams.Password,
                Port = configParams.Port
            };
        }

        public IConnection Connection()
        {
            if (_conection == null) _conection = ConnectionFactory.CreateConnection();
            return _conection;
        }

        public IModel Channel()
        {
            if (_channel == null) _channel = Connection().CreateModel();
            return _channel;
        }
    }
}