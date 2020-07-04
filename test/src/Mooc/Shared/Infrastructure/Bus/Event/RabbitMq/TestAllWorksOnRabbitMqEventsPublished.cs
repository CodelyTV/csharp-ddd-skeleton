namespace CodelyTv.Test.Mooc.Shared.Infrastructure.Bus.Event.RabbitMq
{
    using System.Threading.Tasks;
    using CodelyTv.Shared.Domain.Bus.Event;
    using CodelyTv.Shared.Domain.Courses;

    public class TestAllWorksOnRabbitMqEventsPublished : IDomainEventSubscriber<CourseCreatedDomainEvent>
    {
        public bool HasBeenExecuted = false;

        public Task On(CourseCreatedDomainEvent domainEvent)
        {
            HasBeenExecuted = true;
            return Task.CompletedTask;
        }
    }
}