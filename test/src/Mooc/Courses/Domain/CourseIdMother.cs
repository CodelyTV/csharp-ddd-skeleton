namespace CodelyTv.Test.Mooc.Courses.Domain
{
    using CodelyTv.Mooc.Courses.Domain;
    using Test.Shared.Domain;

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