namespace CodelyTv.Tests.Mooc.Courses.Infrastructure.Persistence
{
    using System.Threading.Tasks;
    using Domain;
    using Newtonsoft.Json;
    using Xunit;

    public class InMemoryCourseRepositoryShould : CoursesModuleInfrastructureTestCase
    {
        [Fact]
        public async Task save_a_course()
        {
            var course = CourseMother.Random();
            await Repository.Save(course);
        }

        [Fact]
        public async Task return_an_existing_course()
        {
            var course = CourseMother.Random();

            await Repository.Save(course);

            Assert.Equal(JsonConvert.SerializeObject(course), JsonConvert.SerializeObject(Repository.Search(course.Id)));
        }

        [Fact]
        public void not_return_a_non_existing_course()
        {
            Assert.Null(Repository.Search(CourseIdMother.Random()));
        }
    }
}