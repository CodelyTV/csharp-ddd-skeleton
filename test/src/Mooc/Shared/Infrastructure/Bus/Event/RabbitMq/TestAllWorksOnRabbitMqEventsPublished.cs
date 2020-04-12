namespace CodelyTv.Test.Mooc.Shared.Infrastructure.Bus.Event.RabbitMq
{
    using System.Threading.Tasks;
    using CodelyTv.Mooc.Courses.Domain;
    using CodelyTv.Shared.Domain.Bus.Event;

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