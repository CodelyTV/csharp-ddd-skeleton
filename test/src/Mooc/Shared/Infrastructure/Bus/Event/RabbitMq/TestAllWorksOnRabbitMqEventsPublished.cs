using System.Threading.Tasks;
using CodelyTv.Shared.Domain.Bus.Event;
using CodelyTv.Shared.Domain.Courses.Domain;

namespace CodelyTv.Test.Mooc.Shared.Infrastructure.Bus.Event.RabbitMq
{
    public class TestAllWorksOnRabbitMqEventsPublished : DomainEventSubscriber<CourseCreatedDomainEvent>
    {
        public bool HasBeenExecuted;

        public Task On(CourseCreatedDomainEvent domainEvent)
        {
            HasBeenExecuted = true;
            return Task.CompletedTask;
        }
    }
}
