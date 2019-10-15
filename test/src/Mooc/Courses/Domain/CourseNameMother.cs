namespace MoocTest.src.Courses.Domain
{
    using Mooc.Courses.Domain;
    using SharedTest.src.Domain;

    public class CourseNameMother
    {
        public static CourseName Create(string value)
        {
            return new CourseName(value);
        }

        public static CourseName Random()
        {
            return Create(WordMother.Random());
        }
    }
}