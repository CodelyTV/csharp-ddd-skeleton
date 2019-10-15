namespace MoocTest.src.Courses.Infrastructure
{
    using Domain;
    using Newtonsoft.Json;
    using Xunit;

    public class FileCourseRepositoryTest : CoursesModuleInfrastructureTestCase
    {
        [Fact]
        public void Save_Course_ItShouldSave()
        {
            var course = CourseMother.Random();

            this.Repository.Save(course);
        }

        [Fact]
        public void Save_Course_ShouldReturnAnExistingCourse()
        {
            var course = CourseMother.Random();

            this.Repository.Save(course);

            Assert.Equal(JsonConvert.SerializeObject(course), JsonConvert.SerializeObject(this.Repository.Search(course.Id)));
        }

        [Fact]
        public void Search_NonExistingId_ItShouldNotReturnANonExistingCourse()
        {
            Assert.Null(this.Repository.Search(CourseIdMother.Random()));
        }
    }
}