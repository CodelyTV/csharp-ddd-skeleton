namespace CodelyTv.Test.Mooc.Courses.Application.Create
{
    using CodelyTv.Mooc.Courses.Application.Create;
    using CodelyTv.Mooc.Courses.Domain;
    using Domain;

    public static class CreateCourseCommandMother
    {
        public static CreateCourseCommand Create(CourseId id, CourseName name, CourseDuration duration)
        {
            return new CreateCourseCommand(id.Value, name.Value, duration.Value);
        }

        public static CreateCourseCommand Random()
        {
            return Create(CourseIdMother.Random(), CourseNameMother.Random(), CourseDurationMother.Random());
        }
    }
}