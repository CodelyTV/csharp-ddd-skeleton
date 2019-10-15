namespace MoocTest.src.Courses.Application.Create
{
    using Domain;
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

            var request = CreateCourseRequestMother.Random();
            var course = CourseMother.FromRequest(request);

            A.CallTo(() => repository.Save(course));

            creator.Invoke(request);
        }
    }
}