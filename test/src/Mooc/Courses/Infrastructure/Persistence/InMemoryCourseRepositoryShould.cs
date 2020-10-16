using System.Threading.Tasks;
using CodelyTv.Test.Mooc.Courses.Domain;
using Newtonsoft.Json;
using Xunit;

namespace CodelyTv.Test.Mooc.Courses.Infrastructure.Persistence
{
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

            Assert.Equal(JsonConvert.SerializeObject(course),
                JsonConvert.SerializeObject(await Repository.Search(course.Id)));
        }

        [Fact]
        public async Task not_return_a_non_existing_course()
        {
            Assert.Null(await Repository.Search(CourseIdMother.Random()));
        }
    }
}
