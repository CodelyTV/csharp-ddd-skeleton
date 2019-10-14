namespace MoocTest.src.Courses.Application.Create
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

            var request = new CreateCourseRequest("decf33ca-81a7-419f-a07a-74f214e928e5", "some-name", "some-duration");

            var course = new Course(new CourseId(request.Id), request.Name, request.Duration);

            A.CallTo(() => repository.Save(course));

            creator.Invoke(request);
        }
    }
}