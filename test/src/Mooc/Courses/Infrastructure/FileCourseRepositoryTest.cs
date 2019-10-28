namespace CodelyTv.Tests.Mooc.Courses.Infrastructure
{
    using System.Threading.Tasks;
    using Domain;
    using Newtonsoft.Json;
    using Xunit;

    public class FileCourseRepositoryTest : CoursesModuleInfrastructureTestCase
    {
        [Fact]
        public async Task Save_Course_ItShouldSave()
        {
            var course = CourseMother.Random();

            await this.Repository.Save(course);
        }

        [Fact]
        public async Task Save_Course_ShouldReturnAnExistingCourse()
        {
            var course = CourseMother.Random();

            await this.Repository.Save(course);

            Assert.Equal(JsonConvert.SerializeObject(course), JsonConvert.SerializeObject(this.Repository.Search(course.Id)));
        }

        [Fact]
        public void Search_NonExistingId_ItShouldNotReturnANonExistingCourse()
        {
            Assert.Null(this.Repository.Search(CourseIdMother.Random()));
        }
    }
}