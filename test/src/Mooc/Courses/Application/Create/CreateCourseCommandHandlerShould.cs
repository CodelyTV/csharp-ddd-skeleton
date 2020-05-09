namespace CodelyTv.Test.Mooc.Courses.Application.Create
{
    using CodelyTv.Mooc.Courses.Application.Create;
    using Domain;
    using Xunit;

    public class CreateCourseCommandHandlerShould : CoursesModuleUnitTestCase
    {
        private readonly CreateCourseCommandHandler _handler;

        public CreateCourseCommandHandlerShould()
        {
            this._handler = new CreateCourseCommandHandler(new CourseCreator(Repository.Object, EventBus.Object));
        }

        [Fact]
        public void create_a_valid_course()
        {
            var command = CreateCourseCommandMother.Random();
            var course = CourseMother.FromRequest(command);
            var domainEvent = CourseCreatedDomainEventMother.FromCourse(course);

            this._handler.Handle(command);

            this.ShouldHaveSave(course);
            this.ShouldHavePublished(domainEvent);
        }
    }
}