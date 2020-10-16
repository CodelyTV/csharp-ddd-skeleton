using CodelyTv.Backoffice.Courses.Domain;

namespace CodelyTv.Backoffice.Courses.Application
{
    public class BackofficeCourseResponse
    {
        public string Id { get; }
        public string Name { get; }
        public string Duration { get; }

        public BackofficeCourseResponse(string id, string name, string duration)
        {
            Id = id;
            Name = name;
            Duration = duration;
        }

        public static BackofficeCourseResponse FromAggregate(BackofficeCourse course)
        {
            return new BackofficeCourseResponse(course.Id, course.Name, course.Duration);
        }
    }
}
