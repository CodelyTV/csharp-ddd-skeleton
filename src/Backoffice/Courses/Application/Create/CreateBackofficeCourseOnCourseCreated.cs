using System.Threading.Tasks;
using CodelyTv.Shared.Domain.Bus.Event;
using CodelyTv.Shared.Domain.Courses.Domain;

namespace CodelyTv.Backoffice.Courses.Application.Create
{
    public class CreateBackofficeCourseOnCourseCreated : DomainEventSubscriber<CourseCreatedDomainEvent>
    {
        private readonly BackofficeCourseCreator _creator;

        public CreateBackofficeCourseOnCourseCreated(BackofficeCourseCreator creator)
        {
            _creator = creator;
        }

        public async Task On(CourseCreatedDomainEvent @event)
        {
            await _creator.Create(@event.AggregateId, @event.Name, @event.Duration);
        }
    }
}
