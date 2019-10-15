namespace MoocTest.src.Courses.Application.Create
{
    using Domain;
    using Mooc.Courses.Application.Create;
    using Mooc.Courses.Domain;

    public static class CreateCourseRequestMother
    {
        public static CreateCourseRequest Create(CourseId id, CourseName name, CourseDuration duration)
        {
            return new CreateCourseRequest(id.Value, name.Value, duration.Value);
        }

        public static CreateCourseRequest Random()
        {
            return Create(CourseIdMother.Random(), CourseNameMother.Random(), CourseDurationMother.Random());
        }
    }
}