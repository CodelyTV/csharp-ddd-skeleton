namespace CodelyTv.Tests.Mooc.CoursesCounter.Application.Increment
{
    using CodelyTv.Mooc.Courses.Domain;
    using CodelyTv.Mooc.CoursesCounter.Application.Incrementer;
    using CodelyTv.Mooc.CoursesCounter.Domain;
    using Courses.Domain;
    using Domain;
    using Xunit;

    public class IncrementCoursesCounterOnCourseCreatedShould : CoursesCounterModuleUnitTestCase
    {
        private IncrementCoursesCounterOnCourseCreated Subscriber;

        public IncrementCoursesCounterOnCourseCreatedShould()
        {
            this.Subscriber = new IncrementCoursesCounterOnCourseCreated(
                new CoursesCounterIncrementer(this.Repository.Object, this.UuidGenerator.Object)
            );
        }

        [Fact]
        public void it_should_initialize_a_new_counter()
        {
            CourseCreatedDomainEvent domainEvent = CourseCreatedDomainEventMother.Random();

            CourseId courseId = CourseIdMother.Create(domainEvent.AggregateId);
            CoursesCounter newCounter = CoursesCounterMother.WithOne(courseId);

            ShouldSearch();
            ShouldGenerateUuid(newCounter.Id.Value);

            this.Subscriber.On(domainEvent);

            this.ShouldHaveSaved(newCounter);
        }

        [Fact]
        public void it_should_increment_an_existing_counter()
        {
            CourseCreatedDomainEvent domainEvent = CourseCreatedDomainEventMother.Random();

            CourseId courseId = CourseIdMother.Create(domainEvent.AggregateId);
            CoursesCounter existingCounter = CoursesCounterMother.Random();
            CoursesCounter incrementedCounter = CoursesCounterMother.Incrementing(existingCounter, courseId);

            ShouldSearch(existingCounter);

            this.Subscriber.On(domainEvent);

            ShouldHaveSaved(incrementedCounter);
        }

        [Fact]
        public void it_should_not_increment_an_already_incremented_course()
        {
            CourseCreatedDomainEvent domainEvent = CourseCreatedDomainEventMother.Random();

            CourseId courseId = CourseIdMother.Create(domainEvent.AggregateId);
            CoursesCounter existingCounter = CoursesCounterMother.WithOne(courseId);

            ShouldSearch(existingCounter);

            this.Subscriber.On(domainEvent);
        }
    }
}