namespace CodelyTv.Tests.Mooc.Courses.Domain
{
    using CodelyTv.Mooc.Courses.Domain;
    using Test.Shared.Domain;

    public class CourseDurationMother
    {
        public static CourseDuration Create(string value)
        {
            return new CourseDuration(value);
        }

        public static CourseDuration Random()
        {
            return Create($"{IntegerMother.Random()} {RandomElementPicker.From("months", "years", "days", "hours", "minutes", "seconds")}");
        }
    }
}