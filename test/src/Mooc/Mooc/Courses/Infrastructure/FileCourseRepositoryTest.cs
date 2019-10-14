namespace MoocTest.src.Courses.Infrastructure
{
    using Mooc.Courses.Domain;
    using Mooc.Courses.Infrastructure;
    using Newtonsoft.Json;
    using Xunit;

    public class FileCourseRepositoryTest
    {
        [Fact]
        public void Save_Course_ItShouldSave()
        {
            var repository = new FileCourseRepository();
            var course = new Course(new CourseId("decf33ca-81a7-419f-a07a-74f214e928e5"), "name", "duration");

            repository.Save(course);
        }

        [Fact]
        public void Save_Course_ShouldReturnAnExistingCourse()
        {
            var repository = new FileCourseRepository();
            var course = new Course(new CourseId("decf33ca-81a7-419f-a07a-74f214e928e5"), "name", "duration");

            repository.Save(course);

            Assert.Equal(JsonConvert.SerializeObject(course), JsonConvert.SerializeObject(repository.Search(course.Id)));
        }

        [Fact]
        public void Search_NonExistingId_ItShouldNotReturnANonExistingCourse()
        {
            var repository = new FileCourseRepository();

            Assert.Null(repository.Search(new CourseId("65cc2174-30bf-4630-9392-f8084f088cc6")));
        }
    }
}