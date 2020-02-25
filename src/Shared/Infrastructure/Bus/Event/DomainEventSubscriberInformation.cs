namespace CodelyTv.Shared.Infrastructure.Bus.Event
{
    public class DomainEventSubscriberInformation
    {
        private readonly object _subscriberClasss;
        private readonly object _subscribedEvents;

        public DomainEventSubscriberInformation(object subscribedEvents, object subscriberClasss)
        {
            _subscribedEvents = subscribedEvents;
            _subscriberClasss = subscriberClasss;
        }

        public string ContextName()
        {
            throw new System.NotImplementedException();
        }

        public string ModuleName()
        {
            throw new System.NotImplementedException();
        }

        public string ClassName()
        {
            throw new System.NotImplementedException();
        }
    }
}