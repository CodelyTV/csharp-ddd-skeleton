namespace CodelyTv.Tests.Mooc.Courses.Application.Create
{
    using CodelyTv.Mooc.Courses.Application.Create;
    using Domain;
    using Xunit;

    public class CourseCreatorTest : CoursesModuleUnitTestCase
    {
        private readonly CourseCreator _creator;

        public CourseCreatorTest()
        {
            this._creator = new CourseCreator(this.Repository.Object, this.EventBus.Object);
        }

        [Fact]
        public void Invoke_ItShouldCreateAValidCourse()
        {
            var request = CreateCourseRequestMother.Random();
            var course = CourseMother.FromRequest(request);
            var domainEvent = CourseCreatedDomainEventMother.FromCourse(course);

            this._creator.Invoke(request);

            this.ShouldHaveSave(course);
            this.ShouldHavePublished(domainEvent);
        }
    }
}