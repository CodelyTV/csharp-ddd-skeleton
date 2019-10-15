namespace MoocTest.src.Courses.Domain
{
    using Mooc.Courses.Domain;
    using SharedTest.src.Domain;

    public class CourseIdMother
    {
        public static CourseId Create(string value)
        {
            return new CourseId(value);
        }

        public static CourseId Random()
        {
            return Create(UuidMother.Random());
        }
    }
}