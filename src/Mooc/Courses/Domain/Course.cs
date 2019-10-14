namespace Mooc.Courses.Domain
{
    public class Course
    {
        public CourseId Id { get; private set; }
        public string Name { get; private set; }
        public string Duration { get; private set; }

        public Course(CourseId id, string name, string duration)
        {
            Id = id;
            Name = name;
            Duration = duration;
        }
    }
}