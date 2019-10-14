namespace MoocTest.src.Courses.Application
{
    using FakeItEasy;
    using Mooc.Courses.Application.Create;
    using Mooc.Courses.Domain;
    using Xunit;

    public class CourseCreatorTest
    {
        [Fact]
        public void Invoke_ItShouldCreateAValidCourse()
        {
            var repository = A.Fake<ICourseRepository>();
            var creator = new CourseCreator(repository);

            var id = "some-id";
            var name = "some-name";
            var duration = "some-duration";

            var course = new Course(id, name, duration);

            A.CallTo(() => repository.Save(course));

            creator.Invoke(new CreateCourseRequest(id, name, duration));
        }
    }
}