namespace CodelyTv.Test.Mooc.Courses.Application.Create
{
    using CodelyTv.Mooc.Courses.Application.Create;
    using Domain;
    using Xunit;

    public class CourseCreatorShould : CoursesModuleUnitTestCase
    {
        private readonly CourseCreator _creator;

        public CourseCreatorShould()
        {
            this._creator = new CourseCreator(this.Repository.Object, this.EventBus.Object);
        }

        [Fact]
        public void create_a_valid_course()
        {
            var request = CreateCourseRequestMother.Random();
            var course = CourseMother.FromRequest(request);
            var domainEvent = CourseCreatedDomainEventMother.FromCourse(course);

            this._creator.Invoke(request);

            this.ShouldHaveSave(course);
            this.ShouldHavePublished(domainEvent);
        }
    }
}