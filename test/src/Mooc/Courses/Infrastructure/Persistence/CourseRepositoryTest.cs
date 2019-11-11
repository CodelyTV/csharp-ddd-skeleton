namespace CodelyTv.Tests.Mooc.Courses.Infrastructure.Persistence
{
    using System.Threading.Tasks;
    using Domain;
    using Newtonsoft.Json;
    using Xunit;

    public class CourseRepositoryTest : CoursesModuleInfrastructureTestCase
    {
        [Fact]
        public async Task Save_Course_ItShouldSave()
        {
            var course = CourseMother.Random();

            var repository = Repository();
            await repository.Save(course);
        }

        [Fact]
        public async Task Save_Course_ShouldReturnAnExistingCourse()
        {
            var course = CourseMother.Random();
            var repository = Repository();

            await repository.Save(course);

            Assert.Equal(JsonConvert.SerializeObject(course), JsonConvert.SerializeObject(repository.Search(course.Id)));
        }

        [Fact]
        public void Search_NonExistingId_ItShouldNotReturnANonExistingCourse()
        {
            var repository = Repository();
            Assert.Null(repository.Search(CourseIdMother.Random()));
        }
    }
}