using CodelyTv.Mooc.CoursesCounters.Application.Incrementer;
using CodelyTv.Test.Mooc.Courses.Domain;
using CodelyTv.Test.Mooc.CoursesCounters.Domain;
using Xunit;

namespace CodelyTv.Test.Mooc.CoursesCounters.Application.Increment
{
    public class IncrementCoursesCounterOnCourseCreatedShould : CoursesCounterModuleUnitTestCase
    {
        private readonly IncrementCoursesCounterOnCourseCreated Subscriber;

        public IncrementCoursesCounterOnCourseCreatedShould()
        {
            Subscriber = new IncrementCoursesCounterOnCourseCreated(
                new CoursesCounterIncrementer(Repository.Object, UuidGenerator.Object)
            );
        }

        [Fact]
        public void it_should_initialize_a_new_counter()
        {
            var domainEvent = CourseCreatedDomainEventMother.Random();

            var courseId = CourseIdMother.Create(domainEvent.AggregateId);
            var newCounter = CoursesCounterMother.WithOne(courseId);

            ShouldSearch();
            ShouldGenerateUuid(newCounter.Id.Value);

            Subscriber.On(domainEvent);

            ShouldHaveSaved(newCounter);
        }

        [Fact]
        public void it_should_increment_an_existing_counter()
        {
            var domainEvent = CourseCreatedDomainEventMother.Random();

            var courseId = CourseIdMother.Create(domainEvent.AggregateId);
            var existingCounter = CoursesCounterMother.Random();
            var incrementedCounter = CoursesCounterMother.Incrementing(existingCounter, courseId);

            ShouldSearch(existingCounter);

            Subscriber.On(domainEvent);

            ShouldHaveSaved(incrementedCounter);
        }

        [Fact]
        public void it_should_not_increment_an_already_incremented_course()
        {
            var domainEvent = CourseCreatedDomainEventMother.Random();

            var courseId = CourseIdMother.Create(domainEvent.AggregateId);
            var existingCounter = CoursesCounterMother.WithOne(courseId);

            ShouldSearch(existingCounter);

            Subscriber.On(domainEvent);
        }
    }
}
