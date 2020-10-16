using CodelyTv.Mooc.Courses.Application.Create;
using CodelyTv.Test.Mooc.Courses.Domain;
using Xunit;

namespace CodelyTv.Test.Mooc.Courses.Application.Create
{
    public class CreateCourseCommandHandlerShould : CoursesModuleUnitTestCase
    {
        private readonly CreateCourseCommandHandler _handler;

        public CreateCourseCommandHandlerShould()
        {
            _handler = new CreateCourseCommandHandler(new CourseCreator(Repository.Object, EventBus.Object));
        }

        [Fact]
        public void create_a_valid_course()
        {
            var command = CreateCourseCommandMother.Random();
            var course = CourseMother.FromRequest(command);
            var domainEvent = CourseCreatedDomainEventMother.FromCourse(course);

            _handler.Handle(command);

            ShouldHaveSave(course);
            ShouldHavePublished(domainEvent);
        }
    }
}
