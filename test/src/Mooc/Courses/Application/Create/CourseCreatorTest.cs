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
            this._creator = new CourseCreator(this.Repository, this.EventBus);
        }

        [Fact]
        public void Invoke_ItShouldCreateAValidCourse()
        {
            var request = CreateCourseRequestMother.Random();
            var course = CourseMother.FromRequest(request);

            this.ShouldSave(course);

            this._creator.Invoke(request);
        }
    }
}