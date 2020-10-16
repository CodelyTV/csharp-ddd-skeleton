namespace CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq
{
    public class RabbitMqConfigParams
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string HostName { get; set; }

        public int Port { get; set; }
    }
}
