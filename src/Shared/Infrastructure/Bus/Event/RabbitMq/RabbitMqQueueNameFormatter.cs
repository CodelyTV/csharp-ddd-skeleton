namespace CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq
{
    using Domain;

    public static class RabbitMqQueueNameFormatter
    {
        public static string Format(DomainEventSubscriberInformation information)
        {
            return
                $"codelytv.{information.ContextName()}.{information.ModuleName()}.{Utils.ToSnake(information.ClassName())}";
        }

        public static string FormatRetry(DomainEventSubscriberInformation information)
        {
            return $"retry.{Format(information)}";
        }

        public static string FormatDeadLetter(DomainEventSubscriberInformation information)
        {
            return $"dead_letter.{Format(information)}";
        }
    }
}