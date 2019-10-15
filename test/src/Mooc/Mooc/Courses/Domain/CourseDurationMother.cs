namespace MoocTest.src.Courses.Domain
{
    using Mooc.Courses.Domain;
    using SharedTest.src.Domain;

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