namespace CodelyTv.Test.Mooc.Courses.Domain
{
    using CodelyTv.Mooc.Courses.Domain;
    using Test.Shared.Domain;

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