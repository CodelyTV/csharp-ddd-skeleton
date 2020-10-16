using CodelyTv.Mooc.Courses.Domain;
using CodelyTv.Test.Shared.Domain;

namespace CodelyTv.Test.Mooc.Courses.Domain
{
    public class CourseDurationMother
    {
        public static CourseDuration Create(string value)
        {
            return new CourseDuration(value);
        }

        public static CourseDuration Random()
        {
            return Create(
                $"{IntegerMother.Random()} {RandomElementPicker.From("months", "years", "days", "hours", "minutes", "seconds")}");
        }
    }
}
