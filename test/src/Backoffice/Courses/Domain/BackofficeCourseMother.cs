using CodelyTv.Backoffice.Courses.Domain;
using CodelyTv.Mooc.Courses.Application.Create;
using CodelyTv.Test.Shared.Domain;

namespace CodelyTv.Test.Backoffice.Courses.Domain
{
    public static class BackofficeCourseMother
    {
        public static BackofficeCourse Create(string id, string name, string duration)
        {
            return new BackofficeCourse(id, name, duration);
        }

        public static BackofficeCourse FromRequest(CreateCourseCommand request)
        {
            return Create(request.Id, request.Name, request.Duration);
        }

        public static BackofficeCourse Random()
        {
            return Create( UuidMother.Random(), WordMother.Random(), RandomDuration());
        }

        public static BackofficeCourse WithName(string name)
        {
            return Create(UuidMother.Random(), name, RandomDuration());
        }

        private static string RandomDuration()
        {
            return $"{IntegerMother.Random()} {RandomElementPicker.From("months", "years", "days", "hours", "minutes", "seconds")}";
        }
    }
}
