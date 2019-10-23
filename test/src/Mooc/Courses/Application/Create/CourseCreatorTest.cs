namespace CodelyTv.Tests.Mooc.Courses.Application.Create
{
    using CodelyTv.Mooc.Courses.Application.Create;
    using Domain;
    using Xunit;

    public class CourseCreatorTest : CoursesModuleUnitTestCase
    {
        private CourseCreator Creator;

        public CourseCreatorTest()
        {
            this.Creator = new CourseCreator(this.Repository);
        }

        [Fact]
        public void Invoke_ItShouldCreateAValidCourse()
        {
            var request = CreateCourseRequestMother.Random();
            var course = CourseMother.FromRequest(request);

            this.ShouldSave(course);

            this.Creator.Invoke(request);
        }
    }
}