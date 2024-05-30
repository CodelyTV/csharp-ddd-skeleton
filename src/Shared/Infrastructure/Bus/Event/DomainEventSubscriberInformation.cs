using System;
using CodelyTv.Shared.Domain;

namespace CodelyTv.Shared.Infrastructure.Bus.Event
{
    public class DomainEventSubscriberInformation
    {
        private readonly Type _subscriberClass;

        public Type SubscribedEvent { get; }

        public string ContextName
        {
            get
            {
                var nameParts = _subscriberClass.FullName?.Split(".");
                return nameParts?.Length > 1 ? nameParts[1] : string.Empty;
            }
        }

        public string ModuleName
        {
            get
            {
                var nameParts = _subscriberClass.FullName?.Split(".");
                return nameParts?.Length > 2 ? nameParts[2] : string.Empty;
            }
        }

        public string ClassName
        {
            get
            {
                var nameParts = _subscriberClass.FullName?.Split(".");
                return nameParts?.Length > 0 ? nameParts[^1] : string.Empty;
            }
        }

        public DomainEventSubscriberInformation(Type subscriberClass, Type subscribedEvent)
        {
            SubscribedEvent = subscribedEvent;
            _subscriberClass = subscriberClass;
        }

        public string FormatRabbitMqQueueName()
        {
            return $"codelytv.{ContextName.ToSnake()}.{ModuleName.ToSnake()}.{ClassName.ToSnake()}";
        }
    }
}
