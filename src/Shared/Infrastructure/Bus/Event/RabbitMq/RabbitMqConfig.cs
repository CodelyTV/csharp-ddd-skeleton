namespace CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq
{
    using Microsoft.Extensions.Options;
    using RabbitMQ.Client;

    public class RabbitMqConfig
    {
        public ConnectionFactory ConnectionFactory { get; private set; }

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
    }
}