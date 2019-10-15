namespace MoocTest.src.Courses.Infrastructure
{
    using Domain;
    using Mooc.Courses.Infrastructure;
    using Newtonsoft.Json;
    using Xunit;

    public class FileCourseRepositoryTest
    {
        [Fact]
        public void Save_Course_ItShouldSave()
        {
            var repository = new FileCourseRepository();
            var course = CourseMother.Random();

            repository.Save(course);
        }

        [Fact]
        public void Save_Course_ShouldReturnAnExistingCourse()
        {
            var repository = new FileCourseRepository();
            var course = CourseMother.Random();

            repository.Save(course);

            Assert.Equal(JsonConvert.SerializeObject(course), JsonConvert.SerializeObject(repository.Search(course.Id)));
        }

        [Fact]
        public void Search_NonExistingId_ItShouldNotReturnANonExistingCourse()
        {
            var repository = new FileCourseRepository();

            Assert.Null(repository.Search(CourseIdMother.Random()));
        }
    }
}