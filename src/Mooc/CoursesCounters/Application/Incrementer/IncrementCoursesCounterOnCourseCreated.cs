using System.Threading.Tasks;
using CodelyTv.Mooc.Courses.Domain;
using CodelyTv.Shared.Domain.Bus.Event;
using CodelyTv.Shared.Domain.Courses.Domain;

namespace CodelyTv.Mooc.CoursesCounters.Application.Incrementer
{
    public class IncrementCoursesCounterOnCourseCreated : DomainEventSubscriber<CourseCreatedDomainEvent>
    {
        private readonly CoursesCounterIncrementer _incrementer;

        public IncrementCoursesCounterOnCourseCreated(CoursesCounterIncrementer incrementer)
        {
            _incrementer = incrementer;
        }

        public async Task On(CourseCreatedDomainEvent @event)
        {
            var courseId = new CourseId(@event.AggregateId);

            await _incrementer.Increment(courseId);
        }
    }
}
