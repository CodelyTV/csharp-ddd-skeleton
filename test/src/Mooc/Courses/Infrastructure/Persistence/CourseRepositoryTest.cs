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
            await Repository.Save(course);
        }

        [Fact]
        public async Task Save_Course_ShouldReturnAnExistingCourse()
        {
            var course = CourseMother.Random();

            await Repository.Save(course);

            Assert.Equal(JsonConvert.SerializeObject(course), JsonConvert.SerializeObject(Repository.Search(course.Id)));
        }

        [Fact]
        public void Search_NonExistingId_ItShouldNotReturnANonExistingCourse()
        {
            Assert.Null(Repository.Search(CourseIdMother.Random()));
        }
    }
}