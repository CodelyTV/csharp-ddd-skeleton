namespace CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq
{
    public class RabbitMqConfigParams
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string HostName { get; set; } = string.Empty;

        public int Port { get; set; }
    }
}
