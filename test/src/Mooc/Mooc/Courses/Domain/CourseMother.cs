namespace MoocTest.src.Courses.Domain
{
    using Mooc.Courses.Application.Create;
    using Mooc.Courses.Domain;

    public class CourseMother
    {
        public static Course Create(CourseId id, CourseName name, CourseDuration duration)
        {
            return new Course(id, name, duration);
        }

        public static Course FromRequest(CreateCourseRequest request)
        {
            return Create(CourseIdMother.Random(), CourseNameMother.Random(), CourseDurationMother.Random());
        }

        public static Course Random()
        {
            return Create(CourseIdMother.Random(), CourseNameMother.Random(), CourseDurationMother.Random());
        }
    }
}