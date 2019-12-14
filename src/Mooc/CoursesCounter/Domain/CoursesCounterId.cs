namespace CodelyTv.Mooc.CoursesCounter.Domain
{
    using CodelyTv.Shared.Domain.ValueObject;

    public class CoursesCounterId : Uuid
    {
        public CoursesCounterId(string value) : base(value)
        {
        }
    }
}