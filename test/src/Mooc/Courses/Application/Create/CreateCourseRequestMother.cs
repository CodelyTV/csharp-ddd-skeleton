namespace CodelyTv.Tests.Mooc.Courses.Application.Create
{
    using CodelyTv.Mooc.Courses.Application.Create;
    using CodelyTv.Mooc.Courses.Domain;
    using Domain;

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