namespace CodelyTv.Mooc.CoursesCounter.Application.Incrementer
{
    using System.Threading.Tasks;
    using CodelyTv.Shared.Domain.Bus.Event;
    using Courses.Domain;

    [DomainEventSubscriber(typeof(CourseCreatedDomainEvent))]
    public class IncrementCoursesCounterOnCourseCreated : IDomainEventSubscriber<CourseCreatedDomainEvent>
    {
        private readonly CoursesCounterIncrementer _incrementer;

        public IncrementCoursesCounterOnCourseCreated(CoursesCounterIncrementer incrementer)
        {
            _incrementer = incrementer;
        }

        public async Task On(CourseCreatedDomainEvent @event)
        {
            CourseId courseId = new CourseId(@event.AggregateId);

            await _incrementer.Increment(courseId);
        }
    }
}