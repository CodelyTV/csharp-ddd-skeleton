namespace CodelyTv.Shared.Infrastructure.Bus.Event
{
    using System;
    using Domain;

    public class DomainEventSubscriberInformation
    {
        private readonly Type _subscriberClass;
        public Type SubscribedEvent { get; private set; }    

        public DomainEventSubscriberInformation(Type subscriberClass, Type subscribedEvent)
        {
            SubscribedEvent = subscribedEvent;
            _subscriberClass = subscriberClass;
        }

        public string ContextName
        {
            get
            {
                string[] nameParts = _subscriberClass.FullName?.Split(".");
                return nameParts?[1];
            }
        }

        public string ModuleName
        {
            get
            {
                string[] nameParts = _subscriberClass.FullName?.Split(".");
                return nameParts?[2];
            }
        }

        public string ClassName
        {
            get
            {
                string[] nameParts = _subscriberClass.FullName?.Split(".");
                return nameParts?[^1];
            }
        }

        public string FormatRabbitMqQueueName()
        {
            return $"codelytv.{this.ContextName.ToSnake()}.{this.ModuleName.ToSnake()}.{this.ClassName.ToSnake()}";
        }
    }
}