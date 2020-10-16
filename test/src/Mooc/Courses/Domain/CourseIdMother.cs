using CodelyTv.Mooc.Courses.Domain;
using CodelyTv.Test.Shared.Domain;

namespace CodelyTv.Test.Mooc.Courses.Domain
{
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
